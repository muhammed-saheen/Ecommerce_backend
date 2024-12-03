namespace Ecommerce_app.Models
{
    public class Generic_response<T>
    {
        public int statuscode { get; set; }

        public  string message { get; set; }
        public bool status { get; set; }

        public T data { get; set; }

    }
}
