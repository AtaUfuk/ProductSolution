using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductApi.Controllers;
using ProductApi.Models;
using ProductApi.Services;
using System.Collections.Generic;
using Xunit;

public class ProductControllerMoqTests
{
    [Fact]
    public void Get_ReturnsListOfProducts()
    {
        var mockService = new Mock<IProductService>();
        mockService.Setup(s => s.GetAll()).Returns(
        [
            new Product { Id = 1, Name = "Mock", Price = 100 }
        ]);

        var controller = new ProductController(mockService.Object);
        var result = controller.Get() as OkObjectResult;

        Assert.NotNull(result);
        var products = result.Value as IEnumerable<Product>;
        Assert.Single(products!);
    }
}
