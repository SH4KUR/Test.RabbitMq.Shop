using Microsoft.EntityFrameworkCore;
using Test.RabbitMq.Shop.Core.Entities;

namespace Test.RabbitMq.Shop.Data;

public class TestShopContext : DbContext
{
    public TestShopContext(DbContextOptions<TestShopContext> options) : base(options) { }
    
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
}