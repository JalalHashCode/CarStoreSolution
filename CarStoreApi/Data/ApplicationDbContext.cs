using CarStoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarStoreApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 

        }
        public DbSet<Car> Cars { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasData(

                new Car()
                {
                    Id = 1,
                    Name = "Foo",
                    Model = "BMW",
                    Price = 3000,
                    Color = "red",
                    ImageUrl = "https://img.freepik.com/premium-photo/photo-supper-shine-bmw-series-stylish-design_1025753-53043.jpg?w=360",
                    CreatedDate = DateTime.Now
                },
                 new Car()
                 {
                     Id = 2,
                     Name = "Foo",
                     Model = "Mercedes",
                     Price = 5500,
                     Color = "black",
                     ImageUrl = "https://img.freepik.com/premium-photo/photo-supper-shine-bmw-series-stylish-design_1025753-52982.jpg?w=996",
                     CreatedDate = DateTime.Now,
                     UpdatedDate = DateTime.Now
                 },
                  new Car()
                  {
                      Id = 3,
                      Name = "Foo",
                      Model = "Volvo",
                      Price = 7000,
                      Color = "bluce",
                      ImageUrl = "https://img.freepik.com/premium-photo/photo-supper-shine-bmw-series-stylish-design_1025753-52413.jpg?w=996",
                      CreatedDate = DateTime.Now
                  },
                   new Car()
                   {
                       Id = 4,
                       Name = "Foo",
                       Model = "Nissan",
                       Price = 1000,
                       Color = "Red",
                       ImageUrl = "https://img.freepik.com/premium-photo/photo-supper-shine-bmw-series-stylish-design_1025753-53043.jpg?w=360",
                       CreatedDate = DateTime.Now
                   },
                    new Car()
                    {
                        Id = 5,
                        Name = "Foo",
                        Model = "Qere",
                        Price = 6000,
                        Color = "Black",
                        ImageUrl = "https://img.freepik.com/premium-photo/photo-supper-shine-bmw-series-stylish-design_1025753-52441.jpg?w=360",
                        CreatedDate = DateTime.Now
                    });

        }
    }
}
