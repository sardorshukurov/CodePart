using DAL.Models;
using Services.DTOs;

namespace Services.Products;

public interface IProductService
{
    Task<IEnumerable<Product>> GetByName(string name);
    Task AddProduct(ProductDTO product);
    Task<Product> EditProduct(Guid id, ProductDTO product);
    Task DeleteProduct(Guid id);
}