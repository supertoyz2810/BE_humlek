using HumlekCoffeeBE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Infrastructure.HumlekCoffeeBEDbContext
{
    public class HCDbContext: DbContext
    {
        public HCDbContext(DbContextOptions<HCDbContext> options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Set the MySQL connection string here
        //    optionsBuilder.UseMySql("server=localhost;database=mydatabase;user=myuser;password=mypassword",
        //        new MySqlServerVersion(new Version(8, 0, 21)));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(x => x.UserName).HasMaxLength(100).IsRequired(true);
                entity.Property(e => e.UserPassword).IsRequired(true);
            });

            modelBuilder.Entity<UserRoleEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne<UserEntity>(fr => fr.User).WithMany(f => f.UserRoles).HasForeignKey(frp => frp.UserId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RestaurantEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(x => x.RestaurantName).HasMaxLength(100).IsRequired(true);
                entity.Property(x => x.RestaurantAddress).HasMaxLength(150);
                entity.Property(e => e.RestaurantLinkImage);
                entity.HasMany<FoodEntity>(fr => fr.Foods).WithOne(x => x.Restaurant).HasForeignKey(frp => frp.RestaurantId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<FoodEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(x => x.FoodName).HasMaxLength(100).IsRequired(true);
                entity.Property(x => x.FoodPrice).HasMaxLength(100).IsRequired(true);
                entity.Property(e => e.FoodLinkImage);
                entity.HasOne<RestaurantEntity>(fr => fr.Restaurant).WithMany(f => f.Foods).HasForeignKey(frp => frp.RestaurantId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<CategoryFoodEntity>(fr => fr.CategoryFood).WithMany().HasForeignKey(frp => frp.CategoryFoodId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CategoryFoodEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(x => x.CategoryFoodName).HasMaxLength(100).IsRequired(true);
            });

            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(x => x.OrderCode).IsUnique();
                entity.HasMany<OrderDetailEntity>(fr => fr.OrderDetails).WithOne(x => x.Order).HasForeignKey(frp => frp.OrderId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OrderDetailEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne<OrderEntity>(fr => fr.Order).WithMany(f => f.OrderDetails).HasForeignKey(frp => frp.OrderId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<UserEntity>(fr => fr.User).WithMany().HasForeignKey(frp => frp.userId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<FoodEntity>(fr => fr.Food).WithMany().HasForeignKey(frp => frp.FoodId).OnDelete(DeleteBehavior.Restrict);
            });
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }
        public DbSet<RestaurantEntity> Restaurants { get; set; }
        public DbSet<FoodEntity> Foods { get; set; }
        public DbSet<CategoryFoodEntity> CategoryFoods { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderDetailEntity> OrderDetails { get; set; }
    }
}
