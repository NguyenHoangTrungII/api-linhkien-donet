using _01_WEBAPI.Data;
using _01_WEBAPI.Helper.ApiResults;
using _01_WEBAPI.Interfaces;
using _01_WEBAPI.Models;
using linhkien_donet.Interfaces;
using linhkien_donet.Models.CartModels;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using linhkien_donet.Dto;
using _01_WEBAPI.Dto;
using linhkien_donet.Entities;
using AutoMapper;
using System.Collections.Generic;

namespace linhkien_donet.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;


        public CartRepository(DataContext context, IMapper mapper = null)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResult<CartDetailDto>> AddToCart(AddToCartModel request)
        {
            try
            {
                var productExist =  await _context.Product
                .Where(c => c.Id == request.ProductId)
                .Include(p => p.Images.Where(i => i.IsThumbail == true))
                .FirstOrDefaultAsync();

                if (productExist != null)
                {
                    var thumbnailImages = productExist.Images.Where(i => i.IsThumbail == true).ToList();

                    var productDto = _mapper.Map<ProductDto>(productExist);

                    productDto.Images = _mapper.Map<List<ImageDto>>(thumbnailImages);


                }
                else 
                {
                    return new ApiFailResult<CartDetailDto>("Product not exists");
                }

                var cart = await _context.Cart.Include(c => c.Items).FirstOrDefaultAsync(c => c.UserId == request.UserId);

                if (cart == null)
                {
                    cart = new Cart
                    {
                        UserId = request.UserId,
                        CreateDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                        Items = new List<CartDetail>()
                    };

                    _context.Cart.Add(cart);
                }

                var cartItem = cart.Items.FirstOrDefault(item => item.ProductId == request.ProductId);

                if (cartItem == null)
                {
                    cartItem = new CartDetail
                    {
                        ProductId = request.ProductId,
                        Quantity = 1,
                        Product = productExist
                    };

                    cart.Items.Add(cartItem);
                }
                else
                {
                    cartItem.Quantity += 1;
                }

                var cartItemDto = _mapper.Map<CartDetailDto>(cartItem);

                await _context.SaveChangesAsync();



                return new ApiSuccessResult<CartDetailDto>(data: cartItemDto);
            }
            catch (Exception ex)
            {
                return new ApiFailResult<CartDetailDto>("Add to Cart Failed" + ex.Message); ;
            }

        }

        public async Task<ApiResult<CartDetailDto>> UpdateCart(UpdateCartModel request)
        {
            try
            {
                var productExist = _mapper.Map<CartDetailDto>(await _context.Product
                                .Where(c => c.Id == request.ProductId)
                                .Include(p => p.Images.Where(i => i.IsThumbail == true))
                                .FirstOrDefaultAsync());

                if (productExist == null)
                {
                    return new ApiFailResult<CartDetailDto>("Product not exists");
                }

                var cart = await _context.Cart.Include(c => c.Items).FirstOrDefaultAsync(c => c.UserId == request.UserId);

                if (cart == null)
                {
                    cart = new Cart
                    {
                        UserId = request.UserId,
                        CreateDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow,
                        Items = new List<CartDetail>()
                    };

                    _context.Cart.Add(cart);
                }

                var cartItem = cart.Items.FirstOrDefault(item => item.ProductId == request.ProductId);

                if (cartItem == null)
                {
                    cartItem = new CartDetail
                    {
                        ProductId = request.ProductId,
                        Quantity = request.Quantity,
                    };

                    cart.Items.Add(cartItem);
                }
                else
                {
                    cartItem.Quantity = request.Quantity;
                }


                var cartDto = _mapper.Map<CartDetailDto>(cartItem);



                await _context.SaveChangesAsync();

                return new ApiSuccessResult<CartDetailDto>(data: cartDto);

            }
            catch (Exception ex)
            {
                return new ApiFailResult<CartDetailDto>("Update Cart Fail cause " + " "+ ex.Message); ;
            }
        }

        public async Task<ApiResult<bool>> RemoveItemFromCart(AddToCartModel request)
        {
            try
            {
                var cart = await _context.Cart
                    .Include(c => c.Items)
                    .FirstOrDefaultAsync(c => c.UserId == request.UserId);

                if (cart == null)
                {
                    return new ApiFailResult<bool>("Cart not found");
                }

                var cartItem = cart.Items.FirstOrDefault(item => item.ProductId == request.ProductId);

                if (cartItem == null)
                {
                    return new ApiFailResult<bool>("Item not found in cart");
                }

                cart.Items.Remove(cartItem);

                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>("Remove Item Sucess");
            }
            catch (Exception ex) { 
                return new ApiFailResult<bool>(ex.Message); 
            }
        }

        public async Task<ApiResult<CartDto>> GetCart(string UserId)
        {
            try { 
                var cart = await _context.Cart
                    .Include(c => c.Items)
                         .ThenInclude(item => item.Product).ThenInclude(ap=>ap.Images.Where(i=>i.IsThumbail ==true))
                    .FirstOrDefaultAsync(c => c.UserId == UserId);


            if (cart == null)
            {
                return new ApiFailResult<CartDto>("Cart not found");
            }

                var cartDto = _mapper.Map<CartDto>(cart);
         
            return new ApiSuccessResult<CartDto>(cartDto);
        }
            catch (Exception ex)
            {
                return new ApiFailResult<CartDto>(ex.Message);
            }
        }

        public async Task<ApiResult<bool>> RemoveAllItem(string userId)
        {
            var cart = await _context.Cart
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                return new ApiFailResult<bool>("No cart found");

            }

            var cartDetail = await _context.CartsDetail.Where(c => c.CartId == cart.Id).ToArrayAsync();

            _context.CartsDetail.RemoveRange(cartDetail);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);

        }



    }
}
