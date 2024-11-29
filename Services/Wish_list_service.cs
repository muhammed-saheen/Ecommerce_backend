using AutoMapper;
using Ecommerce_app.Context;
using Ecommerce_app.DTO;
using Ecommerce_app.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_app.Services
{
    public interface Iwishlist
    {
        public Task<Result> Add_to_wishlist(Guid id, Guid userid);
        public Task<ICollection<Wishlist_dto>> Get_item_wishlist(Guid userid);
        public Task<Result> Remove_wishlist(Guid id);


    }
    public class Wish_list_service : Iwishlist
    {
        private readonly Application_context context;
        private readonly IMapper mapper;
        public Wish_list_service(Application_context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<Result> Add_to_wishlist(Guid id, Guid userid)
        {
            var product = await context.products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return new Result { Message = "product not found", Statuscode = 404 };
            }
            var exist = await context.whishlists.FirstOrDefaultAsync(x => x.Id == id && x.Userid == userid);
            if (exist != null)
            {
                return new Result { Message = "product is already in wishlist", Statuscode = 400 };
            }
            var list = new Whishlist
            {
                Id = new Guid(),
                Productid = id,
                Userid = userid
            };

            await context.whishlists.AddAsync(list);
            await context.SaveChangesAsync();
            return new Result { Message = "product added to wishlist successfully", Statuscode = 200 };

        }

        public async Task<ICollection<Wishlist_dto>> Get_item_wishlist(Guid userid)
        {
            var response = await context.whishlists.Where(w => w.Userid == userid).Include(x=>x.Product).ToListAsync();
            var mapped = mapper.Map<ICollection<Wishlist_dto>>(response);
            return mapped;
        }

        public async Task<Result> Remove_wishlist(Guid id)
        {
            var response = await context.whishlists.FirstOrDefaultAsync(x => x.Id == id);
            if (response == null)  return new Result { Message = "item not found", Statuscode = 404 };

            context.whishlists.Remove(response);
            context.SaveChanges();
            return new Result { Message = "item removed from wishlist", Statuscode = 200 };
        }


    }
}
