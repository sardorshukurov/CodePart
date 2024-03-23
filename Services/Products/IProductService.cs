using DAL.Models;
using Services.DTOs;

namespace Services.Products;

public interface IProductService
{
    /// <summary>
    /// Get all method
    /// </summary>
    /// <returns>All products from db</returns>
    Task<IEnumerable<Product>> GetAll();
    /// <summary>
    /// Get by name method
    /// </summary>
    /// <param name="name">Search parameter</param>
    /// <returns>List of products that match filtration parameter</returns>
    Task<IEnumerable<Product>> GetByName(string name);
    /// <summary>
    /// Add product
    /// </summary>
    /// <param name="product">ProductDTO with name and description</param>
    /// <returns></returns>
    Task AddProduct(ProductDTO product);
    /// <summary>
    /// Edit product
    /// </summary>
    /// <param name="id">Id of the product</param>
    /// <param name="product">Product object</param>
    /// <returns>The product that was edited</returns>
    Task<Product> EditProduct(Guid id, ProductDTO product);
    /// <summary>
    /// Delete product
    /// </summary>
    /// <param name="id">Id of the product</param>
    /// <returns></returns>
    Task DeleteProduct(Guid id);
    /// <summary>
    /// Get one product by id
    /// </summary>
    /// <param name="id">id of the product</param>
    /// <returns></returns>
    Task<Product> GetProductById(Guid id);
}