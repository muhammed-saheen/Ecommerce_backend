using AutoMapper;
using Ecommerce_app.Context;
using Ecommerce_app.DTO;
using Ecommerce_app.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_app.Services
{
    public interface Icategory_service
    {
        public Task<Result> Add_category(Category_dto data);
        public Task<Result> Remove_category(string name);

    }
    public class Category_service:Icategory_service
    {
        private readonly Application_context context;
        private readonly IMapper mapper;
        public Category_service(Application_context context,IMapper mapper) {
            this.context = context;
            this.mapper = mapper;
            
        }

        public async Task<Result> Add_category(Category_dto data)
        {
         
            var exist = await context.categories.FirstOrDefaultAsync(x => x.name == data.name);
            if (exist != null) {
                return new Result { Message = "the category already exist", Statuscode = 400 };
            }
            var mapped =  mapper.Map<Category>(data);
            await context.categories.AddAsync(mapped);
            await context.SaveChangesAsync();
            return new Result { Message = "category added successfully ", Statuscode = 200 };

        }

        public async Task<Result> Remove_category(string name)
        {
            var exist = await context.categories.FirstOrDefaultAsync(x=>x.name==name);
            if (exist == null)
            {
                return new Result { Message = "category doesnot exist", Statuscode = 404 };
            }
            
            context.categories.Remove(exist);
            await context.SaveChangesAsync();
            return new Result { Statuscode = 200,Message="category deleted successfully" };
        }
        
    }
}
