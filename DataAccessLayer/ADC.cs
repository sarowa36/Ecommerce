using DataAccessLayer.Helpers;
using DataAccessLayer.Interceptions;
using EntityLayer.Base;
using EntityLayer.Entities;
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
            modelBuilder.Entity<ShoppingCartItem>().HasKey(x => new { x.ProductId, x.UserId });
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
