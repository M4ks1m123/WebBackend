using WebBlazor.Data.Models;

namespace WebBlazor.Services;

public interface IUserProvider
{
    Task<List<User>> GetAll();
    Task<User> GetOne(int id);
    Task<bool> Add(User item);
    Task<User> Edit(UserDTO item);
    Task<bool> Remove(int id);
}