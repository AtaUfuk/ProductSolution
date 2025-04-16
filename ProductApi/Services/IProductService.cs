using ProductApi.Models;
using System.Collections.Generic;

namespace ProductApi.Services;

public interface IProductService
{
    IEnumerable<Product> GetAll();
    Product? GetById(int id);
    Product Add(Product product);
}
