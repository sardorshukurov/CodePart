using Common.Exceptions;
using DAL.Context;
using DAL.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Services.DTOs;

namespace Services.Products;

public class ProductService : IProductService
{
    private readonly MyDbContext _context;

    public ProductService(MyDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Product>> GetByName(string name)
    {
        var products = await _context.Products
            .Where(p => p.Name.Contains(name))
            .ToListAsync();
        
        return products;
    }

    public async Task AddProduct(ProductDTO product)
    {
         await _context.Products.AddAsync(product.Adapt<Product>());
         await _context.SaveChangesAsync();
    }

    public async Task<Product> EditProduct(Guid id, ProductDTO product)
    {
        var foundProduct = await GetProductById(id);

        _context.Entry(foundProduct).State = EntityState.Modified;

        foundProduct.Name = product.Name;
        foundProduct.Description = product.Description;

        await _context.SaveChangesAsync();

        return await GetProductById(id);
    }

    public async Task DeleteProduct(Guid id)
    {
        var product = await GetProductById(id);

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    private async Task<Product> GetProductById(Guid id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            throw new NotFoundException($"Product with ID {id} not found.");
        }

        return product;
    }
}