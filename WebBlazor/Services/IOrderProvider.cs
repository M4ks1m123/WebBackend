using WebBlazor.Data.Models;

namespace WebBlazor.Services;

public interface IOrderProvider
{
    Task<List<Order>> GetAll();
    Task<List<Order>> GetAllByUser(int UserId);
    Task<Order> GetOne(int id);
    Task<bool> Add(OrderDTO item);
    Task<Order> Edit(Order item);
    Task<bool> Remove(int id);
}