
namespace Ecommerce_app.Exception_handler
{
    public class Global_exception_handler : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex) { 
             context.Response.StatusCode = 500;
             context.Response.ContentType = "text/plain";
             context.Response.WriteAsync($"err:{ex}");
            }
        }
    }
}
