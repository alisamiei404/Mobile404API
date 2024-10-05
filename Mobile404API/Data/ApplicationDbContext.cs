using Microsoft.EntityFrameworkCore;
using Mobile404API.Extention;
using Mobile404API.Models;

namespace Mobile404API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductToShop>().HasKey(ar => new { ar.ProductId, ar.ShopId });

            modelBuilder.Entity<Brand>().HasData(
                new Brand() { Id = 1, Name = "سامسونگ", Slug = "samsung", ImageFileName= "samsung.jpg" },
                new Brand() { Id = 2, Name = "نوکیا", Slug = "nokia", ImageFileName= "nokia.jpg" },
                new Brand() { Id = 3, Name = "شیائومی", Slug = "xiaomi", ImageFileName= "xiaomi.jpg" },
                new Brand() { Id = 4, Name = "اپل", Slug = "apple", ImageFileName= "apple.jpg" },
                new Brand() { Id = 5, Name = "هوآوی", Slug = "huawei", ImageFileName= "huawei.jpg" }
            );

            modelBuilder.Entity<Shop>().HasData(
                new Shop() { Id = 1, UserId = 1, Name = "موبایل404",  CreatedAt = DateTime.Now },
                new Shop() { Id = 2, UserId = 2, Name = "علی شاپ",  CreatedAt = DateTime.Now }
            );

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    Name = "ادمین",
                    Email = "admin@gmail.com",
                    Role = "admin",
                    Password = PasswordHelper.EncodePasswordMd5("1234"),
                    CreatedAt = DateTime.Now
                },
                new User()
                {
                    Id = 2,
                    Name = "علی",
                    Email = "ali@gmail.com",
                    Role = "seller",
                    Password = PasswordHelper.EncodePasswordMd5("1234"),
                    CreatedAt = DateTime.Now
                },
                new User()
                {
                    Id = 3,
                    Name = "تارا",
                    Email = "tara@gmail.com",
                    Role = "client",
                    Password = PasswordHelper.EncodePasswordMd5("1234"),
                    CreatedAt = DateTime.Now
                }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    Title = "گوشی موبایل سامسونگ مدل Galaxy A14",
                    BrandId = 1,
                    Description = "",
                    Ram = 4,
                    Hard = 64,
                    Camera = 50,
                    CreatedAt = DateTime.Now
                },
                 new Product()
                 {
                     Id = 2,
                     Title = "گوشی موبایل شیائومی مدل Redmi 12",
                     BrandId = 3,
                     Description = "",
                     Ram = 8,
                     Hard = 256,
                     Camera = 50,
                     CreatedAt = DateTime.Now
                 },
                  new Product()
                  {
                      Id = 3,
                      Title = "گوشی موبایل هوآوی مدل nova Y71",
                      BrandId = 5,
                      Description = "",
                      Ram = 8,
                      Hard = 128,
                      Camera = 48,
                      CreatedAt = DateTime.Now
                  },
                new Product()
                {
                    Id = 4,
                    Title = "گوشی موبایل شیائومی مدل Redmi Note 13 4G",
                    BrandId = 3,
                    Description = "",
                    Ram = 8,
                    Hard = 256,
                    Camera = 108,
                    CreatedAt = DateTime.Now
                },
                new Product()
                {
                    Id = 5,
                    Title = "گوشی موبایل سامسونگ مدل Galaxy A05",
                    BrandId = 1,
                    Description = "",
                    Ram = 4,
                    Hard = 64,
                    Camera = 50,
                    CreatedAt = DateTime.Now
                },
                new Product()
                {
                    Id = 6,
                    Title = "گوشی موبایل سامسونگ مدل Galaxy M13",
                    BrandId = 1,
                    Description = "",
                    Ram = 4,
                    Hard = 64,
                    Camera = 50,
                    CreatedAt = DateTime.Now
                },
                new Product()
                {
                    Id = 7,
                    Title = "گوشی موبایل سامسونگ مدل Galaxy A15",
                    BrandId = 1,
                    Description = "",
                    Ram = 4,
                    Hard = 128,
                    Camera = 50,
                    CreatedAt = DateTime.Now
                },
                new Product()
                {
                    Id = 8,
                    Title = "گوشی موبایل سامسونگ مدل Galaxy A05s",
                    BrandId = 1,
                    Description = "",
                    Ram = 6,
                    Hard = 128,
                    Camera = 50,
                    CreatedAt = DateTime.Now
                },
                new Product()
                {
                    Id = 9,
                    Title = "گوشی موبایل سامسونگ مدل Galaxy A23 ",
                    BrandId = 1,
                    Description = "",
                    Ram = 4,
                    Hard = 64,
                    Camera = 50,
                    CreatedAt = DateTime.Now
                },
                new Product()
                {
                    Id = 10,
                    Title = "گوشی موبایل سامسونگ مدل Galaxy A25",
                    BrandId = 1,
                    Description = "",
                    Ram = 6,
                    Hard = 128,
                    Camera = 50,
                    CreatedAt = DateTime.Now
                },
                new Product()
                {
                    Id = 11,
                    Title = "گوشی موبایل سامسونگ مدل Galaxy A34 5G ",
                    BrandId = 1,
                    Description = "",
                    Ram = 8,
                    Hard = 128,
                    Camera = 48,
                    CreatedAt = DateTime.Now
                },
                new Product()
                {
                    Id = 12,
                    Title = "گوشی موبایل سامسونگ مدل Galaxy A35",
                    BrandId = 1,
                    Description = "",
                    Ram = 8,
                    Hard = 128,
                    Camera = 50,
                    CreatedAt = DateTime.Now
                },
                new Product()
                {
                    Id = 13,
                    Title = "گوشی موبایل سامسونگ مدل Galaxy A54 5G",
                    BrandId = 1,
                    Description = "",
                    Ram = 8,
                    Hard = 128,
                    Camera = 50,
                    CreatedAt = DateTime.Now
                },
                new Product()
                {
                    Id = 14,
                    Title = "گوشی موبایل سامسونگ مدل Galaxy A55",
                    BrandId = 1,
                    Description = "",
                    Ram = 8,
                    Hard = 256,
                    Camera = 50,
                    CreatedAt = DateTime.Now
                },
                new Product()
                {
                    Id = 15,
                    Title = "گوشی موبایل سامسونگ مدل Galaxy S21 FE 5G",
                    BrandId = 1,
                    Description = "",
                    Ram = 8,
                    Hard = 256,
                    Camera = 12,
                    CreatedAt = DateTime.Now
                },
                new Product()
                {
                    Id = 16,
                    Title = "گوشی موبایل سامسونگ مدل Galaxy S23 FE",
                    BrandId = 1,
                    Description = "",
                    Ram = 8,
                    Hard = 256,
                    Camera = 50,
                    CreatedAt = DateTime.Now
                },
                new Product()
                {
                    Id = 17,
                    Title = "گوشی موبایل سامسونگ مدل Galaxy S23 Ultra",
                    BrandId = 1,
                    Description = "",
                    Ram = 12,
                    Hard = 256,
                    Camera = 200,
                    CreatedAt = DateTime.Now
                },
                new Product()
                {
                    Id = 18,
                    Title = "گوشی موبایل سامسونگ مدل Galaxy S24 Ultra",
                    BrandId = 1,
                    Description = "",
                    Ram = 12,
                    Hard = 512,
                    Camera = 200,
                    CreatedAt = DateTime.Now
                },
                new Product()
                {
                    Id = 19,
                    Title = "گوشی موبایل شیائومی مدل Redmi Note 12S",
                    BrandId = 3,
                    Description = "",
                    Ram = 8,
                    Hard = 256,
                    Camera = 108,
                    CreatedAt = DateTime.Now
                },
                new Product()
                {
                    Id = 20,
                    Title = "گوشی موبایل شیائومی مدل Redmi Note 13 5G",
                    BrandId = 3,
                    Description = "",
                    Ram = 8,
                    Hard = 256,
                    Camera = 108,
                    CreatedAt = DateTime.Now
                },
                new Product()
                {
                    Id = 21,
                    Title = "گوشی موبایل شیائومی مدل Redmi Note 12 Pro 5G",
                    BrandId = 3,
                    Description = "",
                    Ram = 12,
                    Hard = 256,
                    Camera = 108,
                    CreatedAt = DateTime.Now
                },
                new Product()
                {
                    Id = 22,
                    Title = "گوشی موبایل شیائومی مدل Redmi Note 12 Turbo 5G",
                    BrandId = 3,
                    Description = "",
                    Ram = 12,
                    Hard = 256,
                    Camera = 64,
                    CreatedAt = DateTime.Now
                },
                new Product()
                {
                    Id = 23,
                    Title = "گوشی موبایل شیائومی مدل Poco F5 5G",
                    BrandId = 3,
                    Description = "",
                    Ram = 12,
                    Hard = 256,
                    Camera = 64,
                    CreatedAt = DateTime.Now
                }
            );

            modelBuilder.Entity<Gallery>().HasData(
                new Gallery() { Id = 1, ProductId = 1, ImageFileName = "samsung-a14-1.jpg" },
                new Gallery() { Id = 2, ProductId = 1, ImageFileName = "samsung-a14-2.jpg" },
                new Gallery() { Id = 3, ProductId = 3, ImageFileName = "huawei-nova-y71-1.jpg" },
                new Gallery() { Id = 4, ProductId = 3, ImageFileName = "huawei-nova-y71-2.jpg" },
                new Gallery() { Id = 5, ProductId = 3, ImageFileName = "huawei-nova-y71-3.jpg" },
                new Gallery() { Id = 6, ProductId = 4, ImageFileName = "xiaomi-redmi-note-13-1.jpg" },
                new Gallery() { Id = 7, ProductId = 5, ImageFileName = "Galaxy-A05-1.jpg" },
                new Gallery() { Id = 8, ProductId = 5, ImageFileName = "Galaxy-A05-2.jpg" },
                new Gallery() { Id = 9, ProductId = 6, ImageFileName = "Galaxy-M13-1.jpg" },
                new Gallery() { Id = 10, ProductId = 6, ImageFileName = "Galaxy-M13-2.jpg" },
                new Gallery() { Id = 11, ProductId = 6, ImageFileName = "Galaxy-M13-3.jpg" },
                new Gallery() { Id = 12, ProductId = 7, ImageFileName = "Galaxy-A15-1.jpg" },
                new Gallery() { Id = 13, ProductId = 7, ImageFileName = "Galaxy-A15-2.jpg" },
                new Gallery() { Id = 14, ProductId = 7, ImageFileName = "Galaxy-A15-3.jpg" },
                new Gallery() { Id = 15, ProductId = 8, ImageFileName = "Galaxy-A05s-1.jpg" },
                new Gallery() { Id = 16, ProductId = 8, ImageFileName = "Galaxy-A05s-2.jpg" },
                new Gallery() { Id = 17, ProductId = 8, ImageFileName = "Galaxy-A05s-3.jpg" },
                new Gallery() { Id = 18, ProductId = 9, ImageFileName = "Galaxy-A23-1.jpg" },
                new Gallery() { Id = 19, ProductId = 9, ImageFileName = "Galaxy-A23-2.jpg" },
                new Gallery() { Id = 20, ProductId = 9, ImageFileName = "Galaxy-A23-3.jpg" },
                new Gallery() { Id = 21, ProductId = 10, ImageFileName = "Galaxy-A25-1.jpg" },
                new Gallery() { Id = 22, ProductId = 10, ImageFileName = "Galaxy-A25-2.jpg" },
                new Gallery() { Id = 23, ProductId = 10, ImageFileName = "Galaxy-A25-3.jpg" },
                new Gallery() { Id = 24, ProductId = 11, ImageFileName = "Galaxy-A34-1.jpg" },
                new Gallery() { Id = 25, ProductId = 11, ImageFileName = "Galaxy-A34-2.jpg" },
                new Gallery() { Id = 26, ProductId = 12, ImageFileName = "Galaxy-A35-1.jpg" },
                new Gallery() { Id = 27, ProductId = 12, ImageFileName = "Galaxy-A35-2.jpg" },
                new Gallery() { Id = 28, ProductId = 13, ImageFileName = "Galaxy-A54-1.jpg" },
                new Gallery() { Id = 29, ProductId = 13, ImageFileName = "Galaxy-A54-2.jpg" },
                new Gallery() { Id = 30, ProductId = 14, ImageFileName = "Galaxy-A55-1.jpg" },
                new Gallery() { Id = 31, ProductId = 14, ImageFileName = "Galaxy-A55-2.jpg" },
                new Gallery() { Id = 32, ProductId = 14, ImageFileName = "Galaxy-A55-3.jpg" },
                new Gallery() { Id = 33, ProductId = 15, ImageFileName = "Galaxy-S21-1.jpg" },
                new Gallery() { Id = 34, ProductId = 16, ImageFileName = "Galaxy-S23-1.jpg" },
                new Gallery() { Id = 35, ProductId = 16, ImageFileName = "Galaxy-S23-2.jpg" },
                new Gallery() { Id = 36, ProductId = 17, ImageFileName = "Galaxy-S23-Ultra-1.jpg" },
                new Gallery() { Id = 37, ProductId = 18, ImageFileName = "Galaxy-S24-Ultra-1.jpg" },
                new Gallery() { Id = 38, ProductId = 18, ImageFileName = "Galaxy-S24-Ultra-2.jpg" },
                new Gallery() { Id = 39, ProductId = 18, ImageFileName = "Galaxy-S24-Ultra-3.jpg" },
                new Gallery() { Id = 40, ProductId = 19, ImageFileName = "Redmi-Note-12S-1.jpg" },
                new Gallery() { Id = 41, ProductId = 19, ImageFileName = "Redmi-Note-12S-2.jpg" },
                new Gallery() { Id = 42, ProductId = 20, ImageFileName = "Redmi-Note-13-1.jpg" },
                new Gallery() { Id = 43, ProductId = 20, ImageFileName = "Redmi-Note-13-2.jpg" },
                new Gallery() { Id = 44, ProductId = 20, ImageFileName = "Redmi-Note-13-3.jpg" },
                new Gallery() { Id = 45, ProductId = 21, ImageFileName = "Redmi-Note-12-1.jpg" },
                new Gallery() { Id = 46, ProductId = 22, ImageFileName = "Redmi-Note-12-Turbo-1.jpg" },
                new Gallery() { Id = 47, ProductId = 22, ImageFileName = "Redmi-Note-12-Turbo-2.jpg" },
                new Gallery() { Id = 48, ProductId = 22, ImageFileName = "Redmi-Note-12-Turbo-3.jpg" },
                new Gallery() { Id = 49, ProductId = 23, ImageFileName = "Poco-F5-1.jpg" },
                new Gallery() { Id = 50, ProductId = 23, ImageFileName = "Poco-F5-2.jpg" },
                new Gallery() { Id = 51, ProductId = 23, ImageFileName = "Poco-F5-3.jpg" }
            );


            modelBuilder.Entity<ProductToShop>().HasData(
                new ProductToShop() { ShopId = 1, ProductId = 1, Price = 6000000 },
                new ProductToShop() { ShopId = 1, ProductId = 2, Price = 6900000 },
                new ProductToShop() { ShopId = 1, ProductId = 3, Price = 6200000 },
                new ProductToShop() { ShopId = 1, ProductId = 4, Price = 8840000 },
                new ProductToShop() { ShopId = 1, ProductId = 5, Price = 8740000 },
                new ProductToShop() { ShopId = 1, ProductId = 6, Price = 5699000 },
                new ProductToShop() { ShopId = 1, ProductId = 7, Price = 6079000 },
                new ProductToShop() { ShopId = 1, ProductId = 8, Price = 6470000 },
                new ProductToShop() { ShopId = 1, ProductId = 9, Price = 7399000 },
                new ProductToShop() { ShopId = 1, ProductId = 10, Price = 9549000 },
                new ProductToShop() { ShopId = 1, ProductId = 11, Price = 12999000 },
                new ProductToShop() { ShopId = 1, ProductId = 12, Price = 15287000 },
                new ProductToShop() { ShopId = 1, ProductId = 13, Price = 16990000 },
                new ProductToShop() { ShopId = 1, ProductId = 14, Price = 20980000 },
                new ProductToShop() { ShopId = 1, ProductId = 15, Price = 21150000 },
                new ProductToShop() { ShopId = 1, ProductId = 16, Price = 25850000 },
                new ProductToShop() { ShopId = 1, ProductId = 17, Price = 57950000 },
                new ProductToShop() { ShopId = 1, ProductId = 19, Price = 8530000 },
                new ProductToShop() { ShopId = 1, ProductId = 20, Price = 11540000 },
                new ProductToShop() { ShopId = 1, ProductId = 21, Price = 12600000 },
                new ProductToShop() { ShopId = 1, ProductId = 22, Price = 18799000 },
                new ProductToShop() { ShopId = 1, ProductId = 23, Price = 17999000 },
                new ProductToShop() { ShopId = 2, ProductId = 2, Price = 6950000 },
                new ProductToShop() { ShopId = 2, ProductId = 20, Price = 11440000 },
                new ProductToShop() { ShopId = 2, ProductId = 10, Price = 9549000 }
            );


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ProductToShop> ProductToShops { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PasswordReset> PasswordResets { get; set; }
    }
}
