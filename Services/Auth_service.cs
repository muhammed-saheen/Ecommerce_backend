using AutoMapper;
using Ecommerce_app.Context;
using Ecommerce_app.DTO;
using Ecommerce_app.jwt_generator;
using Ecommerce_app.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_app.Services
{
    public interface IAuth_service
    {
        public Task<Result> Login(Login_dto data);
        public Task<Result> Register_user(Register_dto data);
    }
    public class Auth_service: IAuth_service
    {
        private readonly Application_context context;
        private readonly IGenerate_jwt token_service;
        private readonly IMapper mapper;
        public Auth_service(Application_context context,IGenerate_jwt jwt_service,IMapper mapper) { 
         this.context = context;
         this.token_service = jwt_service;
        this.mapper = mapper;
        }
        public async Task<Result> Login(Login_dto data)
        {
            var response = await context.users.FirstOrDefaultAsync(x => x.username == data.Username);

            try
            {
                if (response == null)
                {

                    return new Result { Statuscode = 404, Message = "user doesnot exist" };
                }

                if (!response.Status)
                {
                    Console.WriteLine($"Status for user {response.username}: {response.Status}");
                    return new Result { Statuscode = 401, Message = "user is blocked" };

                }
              
                var checkpassword = Verifypassword(data.Password, response.Password);
                if (!checkpassword)
                {
                    return new Result
                    {
                        Message = "Password deosnt match",
                        Statuscode = 401
                    };
                }
                var token = token_service.Generate_jwt_method(response);
                return new Result { Message=token,Statuscode=200};
            }
            catch (Exception ex) { 
            Console.WriteLine(ex.Message);
            return new Result { Message=ex.Message,Statuscode=500} ;
            }

        }

        public async Task<Result> Register_user(Register_dto data)
        {
            try
            {

                var username_exist = context.users.FirstOrDefault(x => x.username==data.Username);
                if (username_exist != null) {
                    return new Result { Statuscode=409,Message="Username already exist"};
                }

                data.Password=Hashpassword(data.Password);
                var mappeddata = mapper.Map<User>(data);
                await context.users.AddAsync(mappeddata);
                await context.SaveChangesAsync();
                var user_ =await context.users.FirstOrDefaultAsync(x => x.username==data.Username);
                var cart = new Cart
                {
                    Userid = user_.Id
                };
                await context.carts.AddAsync(cart);
                await context.SaveChangesAsync();
                return new Result { Message="Usre registered successfully",Statuscode=200};
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return new Result { Statuscode=500,Message=ex.Message};
            }
        }

        public string Hashpassword(string password)
        {
            string hasshed_password=BCrypt.Net.BCrypt.HashPassword(password);
            return hasshed_password;
        }

        public bool Verifypassword(string password,string hashedpassword)
        {
            bool verifiedpassword = BCrypt.Net.BCrypt.Verify(password,hashedpassword);
            return verifiedpassword;
        }
    }
}
