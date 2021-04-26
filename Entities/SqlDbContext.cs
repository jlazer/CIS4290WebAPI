﻿using Microsoft.EntityFrameworkCore;


namespace WebAPI.Entities
{
    public class SqlDbContext : DbContext
    {
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        //public DbSet<Review> Review { get; set; } *havent created review interaction yet. 

        public SqlDbContext(DbContextOptions<SqlDbContext> options)
        : base(options)
        {
        }
    }
}
