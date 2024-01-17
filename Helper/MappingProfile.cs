using _01_WEBAPI.Dto;
using AutoMapper;
using linhkien_donet.Dto;
using linhkien_donet.Entities;

namespace _01_WEBAPI.Helper
{
    public class MappingProfile : Profile
    {
        private readonly IMapper _mapper;

        public MappingProfile()
        {


            CreateMap<Brand, BrandDto>();
            CreateMap<BrandDto, Brand>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<ImageDto, Images>();
            CreateMap<Images, ImageDto>();

            CreateMap<CartDto, Cart>();
            CreateMap<Cart, CartDto>();

            CreateMap<CartDetailDto, Cart>();
            CreateMap<Cart, CartDetailDto>();

            CreateMap<Product, CartDetailDto>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => _mapper.Map<ProductDto>(src)));
            //CreateMap<Cart, CartDto>()
            //    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => _mapper.Map<List<CartDetailDto>>(src.Items)));
            CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));


            CreateMap<CartDetail, CartDetailDto>()
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

            CreateMap<Cart, CartDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

          



        }
    }
}
