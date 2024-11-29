using Ecommerce_app.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_app.Context
{
    public class Application_context:DbContext
    {

        public Application_context(DbContextOptions<Application_context> options):base(options) { }

        public DbSet<User> users { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Cart> carts { get; set; }

        //public DbSet<Cart_item> cartitem { get; set; }
        public DbSet<Cart_item> cart_Items { get; set; }
        public DbSet<Category> categories { get; set; }

        public DbSet<Whishlist> whishlists { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Address> addresses { get; set; }

        public DbSet<Admin_dashboard> AdminDashboard { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User_configure(modelBuilder);
            Category_configure(modelBuilder);
            Product_Configure(modelBuilder);
            Wishlist_configure(modelBuilder);
            Cart_itemconfigure(modelBuilder);
            Order_itemconfigure(modelBuilder);
        }


        //user configuration

        public void User_configure(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<User>().HasData(
                new User { Id = Guid.Parse("ffd7b3b8-50cd-4f7b-b8c1-d3d08f8bc53f"), Name = "saheen", Email = "sn@gmail.com", username = "saheen", Password = "$2a$11$Y7DBN5.aoCG5gPLYMDQJ1eapvNAsZL7H1mVn190TTAAs96dVmfNee", Role = "admin", Status = true, Phone = "8606524321" }
            );
            modelbuilder.Entity<User>().HasKey(x=>x.Id);
        }


        //category configuration 

      

        public void Category_configure(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Category>().HasIndex(x => x.name).IsUnique(); 

        }

        //product 

        public void Product_Configure(ModelBuilder modelbuilder) {

            modelbuilder.Entity<Product>().HasOne(s => s.category).WithMany(x => x.products).HasForeignKey(s => s.categoryid);
            modelbuilder.Entity<Product>().Property(x => x.Price).HasPrecision(10, 2);
        }

        //wishlist

        public void Wishlist_configure(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Whishlist>().HasOne(x => x.Product).WithMany(y => y.whishlists).HasForeignKey(f => f.Productid);
        }

        //cartitem

        public void Cart_itemconfigure(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<User>().HasOne(x=>x.cart).WithOne(y=>y.User).HasForeignKey<Cart>(f=>f.Userid);
            modelbuilder.Entity<Cart>().HasMany(x => x.cart_Items).WithOne(y => y.Cart).HasForeignKey(f => f.cart_id);
            modelbuilder.Entity<Cart>().Property(x=>x.totalprice).HasPrecision(10, 2);
            modelbuilder.Entity<Cart_item>().HasOne(x => x.product).WithMany(y => y.cartItem).HasForeignKey(f => f.productid);
            modelbuilder.Entity<Cart_item>().Property(x=>x.price).HasPrecision(10, 2);
        }

        //order 

        public void Order_itemconfigure(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Order>().HasOne(x=>x.user).WithMany(y=>y.orders).HasForeignKey(f => f.userid);
            modelbuilder.Entity<Order>().HasOne(x => x.address).WithMany().HasForeignKey(f => f.addressid);
            modelbuilder.Entity<Order>().HasOne(x=>x.product).WithMany().HasForeignKey(f => f.productid);
            modelbuilder.Entity<Order>().Property(x => x.total_price).HasPrecision(10, 2);
        }

    }

}
