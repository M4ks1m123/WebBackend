using Microsoft.EntityFrameworkCore;
using System;


public class ProductService
{
    private FurnitureContext _context;
    public ProductService(FurnitureContext context)
    {
        _context = context;
    }
    public async Task<Product?> AddProduct(ProductDTO product)
    {
        Product nproduct = new Product
        {
            Name = product.Name,
            Desc = product.Desc,
            ImgUrl = product.ImgUrl,
            Price = product.Price,
            Category = await _context.Categories.FindAsync(product.CategoryId)
        };

        if (product.orderIds.Any())
        {
            nproduct.Orders = _context.Orders.ToList().IntersectBy(product.orderIds, order => order.Id).ToList();
        }


        var result = _context.Products.Add(nproduct);
        await _context.SaveChangesAsync();
        return await Task.FromResult(result.Entity);
    }
    public async Task<List<Product>> GetProducts()
    {
        var result = await _context.Products.Include(a => a.Orders).Include(b => b.Category).ToListAsync();
        return await Task.FromResult(result);
    }
    public async Task<List<Product>> GetProductsByCategory(string categoryUrl)
    {
        var result = await _context.Products.Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower())).ToListAsync();
        return await Task.FromResult(result);
    }
    public async Task<Product?> GetProduct(int id)
    {

        var result = _context.Products.Include(a => a.Orders).FirstOrDefault(product => product.Id == id);

        return await Task.FromResult(result);
    }
    public async Task<Product?> UpdateProduct(int id, Product updatedProduct)
    {
        var product = await _context.Products.Include(product => product.Orders).FirstOrDefaultAsync(pr => pr.Id == id);
        if (product != null)
        {
            product.Name = updatedProduct.Name;
            product.Desc = updatedProduct.Desc;
            product.ImgUrl = updatedProduct.ImgUrl;
            product.Price = updatedProduct.Price;
            product.Category = await _context.Categories.FindAsync(updatedProduct.Category);
            if (updatedProduct.Orders.Any())
            {
                product.Orders = _context.Orders.ToList().IntersectBy(updatedProduct.Orders, order => order).ToList();
            }
            _context.Products.Update(product);
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }

        return null;
    }
    public async Task<bool> DeleteProduct(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(pr => pr.Id == id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}