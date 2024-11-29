using AutoMapper;
using Ecommerce_app.Context;
using Ecommerce_app.DTO;
using Ecommerce_app.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_app.Services
{
    public interface Iuser_service{
        public Task<IEnumerable<User_dto>> Get_all_users();
        public  Task<Result> Delete_user(Guid id);
        public  Task<Result> Block_user(Guid id);
        public Task<User_dto> Getuser_byid(Guid id);
    }
    public class User_service:Iuser_service
    {
        private readonly Application_context context;
        private readonly IMapper mapper;
        public User_service(Application_context context,IMapper mapper) {
            this.context = context;
           this.mapper = mapper;
        }

        public async Task<IEnumerable<User_dto>> Get_all_users()
        {
            var response = await context.users.ToListAsync();
            var mapped = mapper.Map<IEnumerable<User_dto>>(response);
            return mapped;
        }

        public async Task<Result> Delete_user(Guid id)
        {
            var response = await context.users.FirstOrDefaultAsync(x => x.Id == id);
            if (response == null) {
                return new Result { Message = "user doesnt exist on id", Statuscode = 404 };
            }
            context.users.Remove(response);
            context.SaveChanges();
            return new Result { Message = "user deleted successfully", Statuscode = 200 };
        }

        public async Task<Result> Block_user(Guid id)
        {
            var response = await context.users.FirstOrDefaultAsync(x => x.Id == id);
            if (response == null)
            {
                return new Result { Message = "user doesnt exist on id", Statuscode = 404 };
            }
            if (response.Role=="admin")
            {
                return new Result { Message = "admin cannot be blocked", Statuscode = 404 };

            }
            response.Status = !response.Status;
            await context.SaveChangesAsync();
            if (response.Status==false)
            {
                return new Result { Message = "user blocked successfully", Statuscode = 200 };
            }
            return new Result { Message = "user is unblocked", Statuscode = 200 };

        }

        public async Task<User_dto> Getuser_byid(Guid id)
        {
            var response = await context.users.FirstOrDefaultAsync(x => x.Id == id);
            if (response == null) { 
             return null;
            }
            var mapped=mapper.Map<User_dto>(response);
            return mapped;
        }
    }
}
