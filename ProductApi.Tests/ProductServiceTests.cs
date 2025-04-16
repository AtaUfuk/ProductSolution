using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models;
using ProductApi.Services;
using System;
using Xunit;

public class ProductServiceTests
{
    private ProductDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ProductDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ProductDbContext(options);
    }

    [Fact]
    public void Add_ShouldAddProduct()
    {
        var db = GetInMemoryDbContext();
        var service = new ProductService(db);

        var product = new Product { Name = "Test", Price = 10 };
        var added = service.Add(product);

        Assert.NotEqual(0, added.Id);
    }
}
