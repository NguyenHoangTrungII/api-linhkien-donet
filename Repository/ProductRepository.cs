using _01_WEBAPI.Data;
using _01_WEBAPI.Dto;
using _01_WEBAPI.Helper.ApiResults;
using AutoMapper;
using CloudinaryDotNet;
using linhkien_donet.Entities;
using linhkien_donet.Helper.ApiResults;
using linhkien_donet.Interfaces;
using linhkien_donet.Models.ProductModels;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;

namespace linhkien_donet.Repository
{
    public class ProductRepository : IProdudctRepository
    {
        private readonly DataContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;


        public ProductRepository(DataContext context, IFileStorageService fileStorageService, ICloudinaryService cloudinaryService, IMapper mapper)
        {
            _context = context;
            _fileStorageService = fileStorageService;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
        }
        public async Task<ApiResult<List<Product>>> GetAllProduct()
        {
        try {
              var allProduct = await _context.Product.OrderBy(p=>p.Id).Include(p => p.Images.Where(i => i.IsThumbail == true)).ToListAsync();

              return new ApiSuccessResult<List<Product>>(allProduct);
            }
        catch ( Exception ex )
            {
               return new ApiFailResult<List<Product>>( ex.Message );
            }
        }

        public async Task<ApiResult<ProductDto>> GetProductById(int id)
        {
            try
            {
                var productById = _mapper.Map<ProductDto>( await _context.Product.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id));

                if( productById == null )
                {
                    return new ApiFailResult<ProductDto>("Not exists");
                }

                return new ApiSuccessResult<ProductDto>(data: productById);
            }
            catch ( Exception ex )
            {
                return new ApiFailResult<ProductDto>( ex.Message );   
            }
        }

        public async Task<ApiResult<List<ProductDto>>> GetProductByName(string name)
        {
            try
            {
                var productByName = _mapper.Map<List<ProductDto>>( await _context.Product.Where(p => p.Name.Contains(name)).Include(p => p.Images).ToListAsync());

                if (productByName == null)
                {
                    return new ApiFailResult<List<ProductDto>>("Not exists");
                }

                return new ApiSuccessResult<List<ProductDto>>(data: productByName);
            }
            catch ( Exception ex )
            {
                return new ApiFailResult<List<ProductDto>>(ex.Message );
            }
        }


        public async Task<PagingApi<List<ProductDto>>> GetPublicProductPaging(GetProductPagingModel request)
        {

            if (request.PageIndex <= 0 || request.PageSize <= 0)
            {
                return new PagingFailResult<List<ProductDto>>("PageIndex and PageSize must be positive values");
            }

            var query = from p in _context.Product
                        join c in _context.Category on p.CategoryId equals c.Id
                        join b in _context.Brand on p.BrandId equals b.Id
                        where p.StatusProduct == 1
                        orderby p.Id descending
                        select new { p, c, b };



            //if (request.keyword != null)
            //{
            //    query = query.Where(x => x.p.Name.Contains(request.keyword.Trim().ToLower()));
            //}

            if (request.categoryId != null)
            {
                query = query.Where(x => x.p.CategoryId.Equals(request.categoryId));
            }

            if (request.brandId != null)
            {
                query = query.Where(x => x.p.BrandId.Equals(request.brandId));
            }

            int totalRecordAll = await query.CountAsync();

            if (query.Count() > 0)
            {
                query = query.Skip(((request.PageIndex - 1) * request.PageSize)).Take(request.PageSize);
            }



            var listProduct = await query.Select(x => new Product()
            {
                Id = x.p.Id,
                Name = x.p.Name,
                Description = x.p.Description,

                CategoryId = x.c.Id,
                BrandId = x.b.Id,
                StatusProduct = x.p.StatusProduct,
                Price = x.p.Price,
                OldPrice = x.p.OldPrice,
                Images = x.p.Images,
            }).ToListAsync();

            var product = _mapper.Map<List<ProductDto>>(listProduct);    

            int totalRecord = await query.CountAsync();
            double totalPage = Math.Ceiling((double)totalRecordAll / request.PageSize);

            return new PagingSuccessResult<List<ProductDto>>(totalRecordAll, totalPage, totalRecord, request.PageSize, request.PageIndex, data: product);
        }


        public async Task<ApiResult<bool>> CreateImages(CreateImagesModel request)
        {
            try
            {
                var product = await _context.Product.FirstOrDefaultAsync(p => p.Id == request.ProductId);
                if(product == null )
                {
                    return new ApiFailResult<bool>("Product don't exists");
                }


                if (request.Image != null)
                {
                    var listProductImage = new List<Images>();
                    foreach (var item in request.Image)
                    {
                        var productImage = new Images()
                        {
                            ProductId = request.ProductId,
                            images = await this.SaveFile(item)
                        };
                        listProductImage.Add(productImage);
                    }
                    await _context.Image.AddRangeAsync(listProductImage);
                    var result = await _context.SaveChangesAsync() > 0;
                    return new ApiSuccessResult<bool>(result);
                }

                return new ApiSuccessResult<bool>("Create images success");
            }
            catch ( Exception ex )
            {
                return new ApiFailResult<bool>( ex.Message );
            }
        }

        public async Task<ApiResult<bool>> CreateImagesByCloudinary(CreateImagesModel request)
        {
            try
            {
                var product = await _context.Product.FirstOrDefaultAsync(p => p.Id == request.ProductId);
                if (product == null)
                {
                    return new ApiFailResult<bool>("Product don't exists");
                }


                if (request.Image != null)
                {
                    var listProductImage = new List<Images>();
                    foreach (var item in request.Image)
                    {
                        var productImage = new Images()
                        {
                            ProductId = request.ProductId,
                            images = _cloudinaryService.UploadPhoto(item)
                        };
                        listProductImage.Add(productImage);
                    }
                    await _context.Image.AddRangeAsync(listProductImage);
                    var result = await _context.SaveChangesAsync() > 0;
                    return new ApiSuccessResult<bool>(result);
                }

                return new ApiSuccessResult<bool>("Create images success");
            }
            catch (Exception ex)
            {
                return new ApiFailResult<bool>(ex.Message);
            }
        }

        public async Task<ApiResult<bool>> DeleteProductImage(int imageId)
        {
            var productImage = await _context.Image.FindAsync(imageId);
            if (productImage == null) return new ApiFailResult<bool>($"Images Id = {imageId} not exists");

            _context.Image.Remove(productImage);
            await _fileStorageService.DeleteFileAsync(productImage.images);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<bool>> CreateProduct(CreateProductModel request)
        {
            var product = new Product()
            {
                Name = request.Name,
                OldPrice = request.OldPrice,
                Description = request.Description,
                Price = request.Price,
                StatusProduct = request.StatusProduct,
                BrandId = request.BrandId,
                CategoryId = request.CategoryId,
            };

            await _context.Product.AddAsync(product);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<bool>> DeleteProduct(int productId)
        {
            var product = await _context.Product.FindAsync(productId);
            if (product == null) return new ApiFailResult<bool>($"Images Id = {productId} not exists");

            var listImageFileRoot = _context.Image.Where(x => x.ProductId == product.Id);

            foreach (var item in listImageFileRoot)
            {
                await _fileStorageService.DeleteFileAsync(item.images);
            }

            _context.Product.Remove(product);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }


        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _fileStorageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

       



    }
}
