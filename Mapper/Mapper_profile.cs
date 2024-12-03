using AutoMapper;
using Ecommerce_app.DTO;
using Ecommerce_app.Models;

namespace Ecommerce_app.Mapper
{
    public class Mapper_profile : Profile
    {
        public Mapper_profile()
        {
            CreateMap<Register_dto, User>().ForMember(x => x.Role, opt => opt.MapFrom(dest => "User"))
           .ForMember(x => x.Status, opt => opt.MapFrom(dest => true));

            //product
            CreateMap<Product, Product_dto>().ForMember(x => x.categoryid, opt => opt.MapFrom(s => s.categoryid));
            CreateMap<Product_dto, Product>();


            //category
            CreateMap<Category_dto, Category>()
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name));

            //user
            CreateMap<User_dto, User>().ReverseMap();
           
            //wishlist 
            CreateMap<Wishlist_dto, Whishlist>().ReverseMap()
             .ForMember(dest => dest.productname, opt => opt.MapFrom(src => src.Product.Name))
             .ForMember(dest => dest.productprice, opt => opt.MapFrom(src => src.Product.OfferPrice));

            //cart

            CreateMap<CartitemSet_dto, Cart_item>();
            CreateMap<Cart, Cart_itemView_dto>();

            CreateMap<Cart_item, Cart_itemView_dto>().ForMember(x => x.Name, opt => opt.MapFrom(src => src.product.Name))
                .ForMember(x => x.price, opt => opt.MapFrom(src => src.price))
                .ForMember(x => x.quantity, opt => opt.MapFrom(src => src.quantity));


            CreateMap<Cart, Cart_view>();
            //order
            CreateMap<Address_set_dto, Address>();
            CreateMap<Order, Orderview_dto>().ForMember(dest => dest.productname, opt => opt.MapFrom(src => src.product.Name))
            .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.address)); 
            
        }
    }
}
