using AutoMapper;
using Ecommerce_app.Context;
using Ecommerce_app.DTO;
using Ecommerce_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_app.Services
{
    public interface ICartitem_service
    {
        public Task<Result> add_cartitem(Guid productid, Guid userid, int quantity);
        public Task<List<Cart_itemView_dto>> get_cartitem(Guid userid);
        public Task<Result> delete_cartitem(Guid itemid, Guid userid);
        public Task<Result> update_cartitem(Guid itemid, CartitemSet_dto data, Guid userid);
        public Task<Result> Checkout(Guid userid, Guid addressid);

    }
    public class Cartitem_service : ICartitem_service
    {
        private readonly IMapper mapper;
        private readonly Application_context context;
        private readonly IOrder_service order_Service;
        public Cartitem_service(Application_context context, IMapper mapper, IOrder_service order_servive)
        {
            this.context = context;
            this.mapper = mapper;
            this.order_Service = order_servive;

        }


        public async Task<Result> add_cartitem(Guid productid, Guid userid, int quantity)
        {
            var cart = await context.carts.FirstOrDefaultAsync(x => x.Userid == userid);
            if (cart == null)
            {
                return new Result { Statuscode = 401, Message = "please login" };
            }
            var product = await context.products.FirstOrDefaultAsync(x => x.Id == productid);
            var cartitem = new Cart_item
            {
                price = product.Price,
                productid = productid,
                cart_id = cart.Id,
                quantity = quantity
            };
            await context.cart_Items.AddAsync(cartitem);
            await context.SaveChangesAsync();
            cart.totalprice = context.cart_Items.Where(x => x.Id == cart.Id).Sum(y => y.product.Price * quantity);
            cart.itemscount = context.cart_Items.Where(x => x.Id == cart.Id).Count();
            await context.SaveChangesAsync();
            return new Result { Message = "item added success", Statuscode = 200 };

        }

        public async Task<List<Cart_itemView_dto>> get_cartitem(Guid userid)
        {
            var cart = await context.carts.FirstOrDefaultAsync(x => x.Userid == userid);
            if (cart == null)
            {
                throw new Exception("Cart not found for the given user.");
            }

            var cartitem = await context.cart_Items
                                        .Where(x => x.cart_id == cart.Id)
                                        .Include(p => p.product)
                                        .ToListAsync();

            var mapped = mapper.Map<List<Cart_itemView_dto>>(cartitem);

            return mapped;
        }


        public async Task<Result> delete_cartitem(Guid itemid, Guid userid)
        {
            var cart = await context.carts.FirstOrDefaultAsync(x => x.Userid == userid);
            var response = await context.cart_Items.FirstOrDefaultAsync(x => x.Id == itemid);
            if (response == null)
            {
                return new Result { Message = "item doesnt exist", Statuscode = 404 };
            }
            context.cart_Items.Remove(response);
            var quantity = response.quantity;
            await context.SaveChangesAsync();
            cart.totalprice = context.cart_Items.Where(x => x.Id == cart.Id).Sum(y => y.product.Price * quantity);
            cart.itemscount = context.cart_Items.Where(x => x.Id == cart.Id).Count();
            await context.SaveChangesAsync();
            return new Result { Message = "item removed successfull", Statuscode = 202 };
        }

        public async Task<Result> update_cartitem(Guid itemid, CartitemSet_dto data, Guid userid)
        {
            var cart = await context.carts.FirstOrDefaultAsync(x => x.Userid == userid);
            if (cart == null)
            {
                return new Result { Message = "please register", Statuscode = 404 };
            }
            var response = await context.cart_Items
                .Include(x => x.product)
                .FirstOrDefaultAsync(x => x.Id == itemid);
            if (data.quantity > response.product.quantity)
            {
                return new Result { Message = $"quantity cannot exceed on stock :{response.product.quantity}" };
            }
            if (response == null)
            {
                return new Result { Message = "item doesnt exist", Statuscode = 404 };
            }
            response.quantity = data.quantity;
            cart.totalprice = context.cart_Items.Where(x => x.Id == cart.Id).Sum(y => y.product.Price * response.quantity);
            response.price = response.quantity * response.price;
            await context.SaveChangesAsync();
            return new Result { Message = "updated successfully ", Statuscode = 200 };
        }


        public async Task<Result> Checkout(Guid userid,Guid addressid)
        {
            var cart = await context.carts.FirstOrDefaultAsync(y => y.Userid == userid);
            var cartitems = await context.cart_Items.Where(x => x.cart_id == cart.Id).ToListAsync();
            if (cartitems.Count() == 0)
            {
                throw new Exception("No cartitem to checkout");
            }
            foreach (Cart_item item in cartitems)
            {
                var response=await order_Service.Place_order(userid,item.productid,item.quantity, addressid);
        
                context.cart_Items.Remove(item);
                
            }
            await context.SaveChangesAsync();
            return new Result { Message ="order placed success ",Statuscode = 200 };
        }

    }
}
