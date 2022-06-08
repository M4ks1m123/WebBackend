using WebBlazor.Data.Models;

namespace WebBlazor.Services;
public interface IProductProvider
{
    Task<List<Product>> GetAll(string? categoryUrl = null);
    Task<Product> GetOne(int id);
    Task<bool> Add(Product item);
    Task<Product> Edit(Product item);
    Task<bool> Remove(int id);
}