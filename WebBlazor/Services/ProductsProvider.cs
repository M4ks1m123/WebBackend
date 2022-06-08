using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using WebBlazor.Data.Models;
using WebBlazor.Services;

namespace WebBlazor.Services;

public class ProductsProvider : IProductProvider
{
    private HttpClient _client;

    public ProductsProvider(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<Product>> GetAll(string? categoryUrl = null)
    {
        var x = categoryUrl == null ?
            await _client.GetFromJsonAsync<List<Product>>("/api/product") :
            await _client.GetFromJsonAsync<List<Product>>($"/api/product/category/{categoryUrl}");
        return x;
    }

    public async Task<Product> GetOne(int id)
    {
        return await _client.GetFromJsonAsync<Product>($"/api/product/{id}");
    }

    public async Task<bool> Add(Product item)
    {
        string data = JsonConvert.SerializeObject(item);
        StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var responce = await _client.PostAsync($"/api/product", httpContent);
        return await Task.FromResult(responce.IsSuccessStatusCode);
    }

    public async Task<Product> Edit(Product item)
    {
        string data = JsonConvert.SerializeObject(item);
        StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var responce = await _client.PutAsync($"/api/product", httpContent);
        Product product = JsonConvert.DeserializeObject<Product>(responce.Content.ReadAsStringAsync().Result);
        return await Task.FromResult(product);
    }

    public async Task<bool> Remove(int id)
    {
        var delete = await _client.DeleteAsync($"/api/product/{id}");

        return await Task.FromResult(delete.IsSuccessStatusCode);
    }

}
