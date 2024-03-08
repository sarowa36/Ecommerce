using ToolsLayer.EF;
using DataAccessLayer.Interceptions;
using EntityLayer.Base;
using EntityLayer.Entities;
using EntityLayer.Entities.JsonDbEntities;
using EntityLayer.M2M;
using EntityLayer.ViewModels.CommonVM;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DataAccessLayer
{
    public class ADC:IdentityDbContext<ApplicationUser>
    {
        public ADC(DbContextOptions<ADC> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new SoftDeletableInterception());
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyGlobalDefaultSqlValue<ICreateDate, DateTime>(x => x.CreateDate, "getdate()");
            modelBuilder.ApplyGlobalFilters<ISoftDeletable>(x => !x.IsDeleted);
            modelBuilder.Entity<Product>().Property(x => x.Images).HasConversion(x=>JsonConvert.SerializeObject(x),x=>JsonConvert.DeserializeObject<List<string>>(x));
            modelBuilder.Entity<Product>().Property(x => x.Variation).HasConversion(x => JsonConvert.SerializeObject(x), x => JsonConvert.DeserializeObject<List<ProductVariation>>(x));
            modelBuilder.Entity<ShoppingCartItem>().Property(x => x.Variation).HasConversion(x => JsonConvert.SerializeObject(x), x => JsonConvert.DeserializeObject<List<SelectedProductVariation>>(x));
            modelBuilder.Entity<Order>().Property(x => x.PaymentResult).HasConversion(x => JsonConvert.SerializeObject(x), x => JsonConvert.DeserializeObject<Iyzipay.Model.CheckoutForm>(x));
            modelBuilder.Entity<OrderRefund>().Property(x => x.RefundResult).HasConversion(x => JsonConvert.SerializeObject(x), x => JsonConvert.DeserializeObject<Iyzipay.Model.Refund>(x));
            modelBuilder.Entity<Order>().Property(x => x.WebhookRequests).HasConversion(x => JsonConvert.SerializeObject(x), x => JsonConvert.DeserializeObject<List<object>>(x));
            modelBuilder.Entity<Order>().Property(x => x.Address).HasConversion(x => JsonConvert.SerializeObject(new {x.CityId,x.Detail,x.DistrictId,x.Id,x.Name,x.UserId,x.Zip }), x => JsonConvert.DeserializeObject<UserAddress>(x));
            modelBuilder.Entity<OrderItem>().Property(x => x.Variation).HasConversion(x => JsonConvert.SerializeObject(x), x => JsonConvert.DeserializeObject<List<SelectedProductVariation>>(x));
            modelBuilder.Entity<OrderRefundM2MOrderItem>().HasKey(x => new { x.OrderItemId, x.OrderRefundId});
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderRefund> OrderRefunds { get; set; }
        public DbSet<OrderRefundM2MOrderItem> OrderRefundM2MOrderItems { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
