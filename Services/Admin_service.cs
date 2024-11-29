using AutoMapper;
using Ecommerce_app.Context;
using Ecommerce_app.Models;
using System.Security.AccessControl;

namespace Ecommerce_app.Services
{
    public interface IAdmin_service
    {
        public  Task<Admin_dashboard> Admin_dashboard();
    }
    public class Admin_service:IAdmin_service
    {
        private readonly IMapper mapper;
        private readonly Application_context context;
        public Admin_service(IMapper mapper,Application_context context) {
        this.mapper = mapper;
        this.context = context;
        }


        public async Task<Admin_dashboard> Admin_dashboard()
        {
            var sales = context.orders.Where(x => x.orderstatus.ToLower() == "delivered").Sum(s=>s.quantity);
            var revenue = context.orders.Where(x => x.orderstatus.ToLower() == "delivered").Sum(p => p.total_price);

            var content = new Admin_dashboard {
                sales = sales,
                revenue = revenue
            };
            await context.AdminDashboard.AddAsync(content);
            await context.SaveChangesAsync();
            return content;
        } 
       
    }
}
