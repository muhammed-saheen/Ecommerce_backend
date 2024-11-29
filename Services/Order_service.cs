using AutoMapper;
using Ecommerce_app.Context;
using Ecommerce_app.DTO;
using Ecommerce_app.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_app.Services
{
     
    public interface IOrder_service
    {
        public Task<Result> Place_order(Guid userid, Guid productid, int quantity, Guid addressid);
        public Task<Result> update_order(Guid orderid, string status);
        public Task<Result> delete_order(Guid orderid);
        public Task<ICollection<Order>> getallorder();
        public  Task<Order> oderby_userid(Guid userid);

        public Task<Result> AddAddress(Address_set_dto data);

        public  Task<Result> RemoveAddress(Guid adressid);


    }
    public class Order_service:IOrder_service
    {
        public readonly IMapper mapper;
        public readonly Application_context context;

        public Order_service(IMapper mapper,Application_context context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<Result> Place_order(Guid userid,Guid productid,int quantity,Guid addressid)
        {
            var product = await context.products.FirstOrDefaultAsync(x => x.Id == productid);
            if (product.quantity>=quantity)
            {
                product.quantity = product.quantity - quantity;
            }
            else
            {
                return new Result { Message = $"Quantity cannot be grater than {product.quantity}", Statuscode = 404 };
            }
            if (product == null) {
                return new Result { Message = "product not found", Statuscode = 404 };
            }
            var address = await context.addresses.FirstOrDefaultAsync(x => x.Id == addressid);
            if (address == null) {
                return new Result { Message = "Address not found", Statuscode = 404 };
            }
            var order = new Order
            {
                userid = userid,
                placed_date = DateTime.Now,
                productid = productid,
                quantity = quantity,
                addressid = addressid,
                orderstatus = "order placed ",
                total_price=product.Price*quantity,
            };

            context.orders.AddAsync(order);
            await context.SaveChangesAsync();
            return new Result { Message = "order placed success", Statuscode = 200 };

        }
       public async Task<Result> update_order(Guid orderid ,string status)
        {
            var order = await context.orders.FirstOrDefaultAsync(x => x.Id == orderid);
            if (order == null) {
                throw new Exception("not an valid order or not exist");
            }
            order.orderstatus = status;
            await context.SaveChangesAsync();
            return new Result { Message ="Status updated success",Statuscode = 200 };

        }

        public async Task<Result> delete_order(Guid orderid)
        {
            var order= await context.orders.FirstOrDefaultAsync(x=>x.Id == orderid);
            if (order == null) {
                return new Result { Message = "order doesnt exist", Statuscode = 404 };
            }
            order.orderstatus = "canceled";
            await context.SaveChangesAsync();
            return new Result { Message="deleted order success ",Statuscode=200 };
        }
        
        public async Task<ICollection<Order>> getallorder()
        {
            var response =await context.orders.ToListAsync();
            return response;
        }
        public async Task<Order> oderby_userid(Guid userid) { 
         var response =await context.orders.FirstOrDefaultAsync(x=>x.userid==userid);
            if (response == null) {
                throw new Exception("order doesnt exist for the user");
            }
            return response;
        }


        public async Task<Result> AddAddress(Address_set_dto data)
        {
            var mapped =mapper.Map<Address>(data);
            await context.addresses.AddAsync(mapped);
            await context.SaveChangesAsync();
            return new Result { Message = "address added success", Statuscode = 200 };
        }

        public async Task<Result> RemoveAddress(Guid adressid)
        {
            var address = await context.addresses.FirstOrDefaultAsync(x => x.Id == adressid);
            if (address == null) {
                return new Result { Message = "address not found", Statuscode = 404 };

            }
            context.Remove(address);
            await context.SaveChangesAsync();
            return new Result { Message = "address removed success", Statuscode = 200 };
        }
    }
}
