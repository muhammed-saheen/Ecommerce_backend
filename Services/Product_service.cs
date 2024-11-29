﻿using AutoMapper;
using Ecommerce_app.Context;
using Ecommerce_app.DTO;
using Ecommerce_app.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Ecommerce_app.Services
{
    public interface IProduct_service
    {
        public Task<ICollection<Product_dto>> Get_allProduct();
        public  Task<Product_dto> Get_product_by_id(Guid id);
        public Task<IEnumerable<Product_dto>> get_product_by_category(string category);

        public Task<ICollection<Product_dto>> Paginated_products(int pagenumber, int pagesize);

        public Task<Result> Delete_product(Guid id);

        public Task<Result> Update_product(Productset_dto data);

        public Task<Result> Add_new_product(Product_dto data);



    }
    public class Product_service:IProduct_service
    {
        private readonly Application_context context;
        private readonly IMapper mapper;
        public Product_service(Application_context context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }

        public async Task<ICollection<Product_dto>> Get_allProduct()
        {
            var response = await context.products.Include(s=>s.category).ToListAsync();
            var mapped = mapper.Map<ICollection<Product_dto>>(response);
            return mapped;
        }

        public async Task<Product_dto> Get_product_by_id(Guid id)
        {
            var response = await context.products.FirstOrDefaultAsync(x => x.Id == id);
            var mapped = mapper.Map<Product_dto>(response);
            return mapped;
        }

        public async Task<IEnumerable<Product_dto>> get_product_by_category(string category)
        {
            var response = await context.products.Where(x => x.category.name == category).Include(x => x.category).ToListAsync();
            var mapped = mapper.Map<IEnumerable<Product_dto>>(response);
            return mapped;
        }

        public async Task<ICollection<Product_dto>> Paginated_products(int pagenumber, int pagesize)
        {
            var response =await context.products.Skip((pagenumber - 1) * pagesize).Take(pagesize).ToListAsync();
            var mapped = mapper.Map<ICollection<Product_dto>>(response);
            return mapped;

        }

        public async Task<Result> Delete_product(Guid id)
        {
            var response = await context.products.FindAsync(id);
            if (response == null)
            {
                return new Result { Message = "product not found", Statuscode = 404 };
            }
            context.products.Remove(response);
            return new Result { Message = $"item deleted success ", Statuscode = 200 };

        }

        public async Task<Result> Update_product(Productset_dto data)
        {
            var response = await context.products.FirstOrDefaultAsync(x => x.Id == data.Id);
            if (response == null)
            {
                return new Result { Message = $"product not found", Statuscode = 404 };
            }
            var category = await context.categories.FirstOrDefaultAsync(x => x.name == data.Category_name);
            if (category == null)
            {
                return new Result { Message = "Category not found", Statuscode = 404 };
            }
            response.Name= data.Name;
            response.Description= data.Description;
            response.quantity = data.quantity;
            response.Price = data.Price;
            response.category =category;
            context.SaveChanges();
            return new Result { Message = "item updated succesfully", Statuscode = 200 };
        }


        public async Task<Result> Add_new_product(Product_dto data)
        {
            var response = await context.categories.FirstOrDefaultAsync(x => x.name == data.categoryname);
            if (response == null) {
                return new Result { Message = "category doesnt exist", Statuscode = 404 };
            }
            var mapped =mapper.Map<Product>(data);
            mapped.category = response;
            await context.products.AddAsync(mapped);
            await context.SaveChangesAsync();
            return new Result { Message = "Product added successfully", Statuscode = 200 };

        }

    }
}