using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using ProductApi.Models;
using Xunit;

public class ProductApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetProducts_ReturnsEmptyInitially()
    {
        var response = await _client.GetAsync("/products");
        response.EnsureSuccessStatusCode();

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();
        Assert.NotNull(products);
        Assert.Empty(products);
    }

    [Fact]
    public async Task PostProduct_AddsAndReturnsProduct()
    {
        var product = new Product { Name = "New Product", Price = 25 };

        var postResponse = await _client.PostAsJsonAsync("/products", product);
        postResponse.EnsureSuccessStatusCode();

        var created = await postResponse.Content.ReadFromJsonAsync<Product>();
        Assert.NotNull(created);
        Assert.Equal("New Product", created!.Name);

        var getResponse = await _client.GetAsync($"/products/{created.Id}");
        getResponse.EnsureSuccessStatusCode();

        var fetched = await getResponse.Content.ReadFromJsonAsync<Product>();
        Assert.NotNull(fetched);
        Assert.Equal(created.Id, fetched!.Id);
    }
}
