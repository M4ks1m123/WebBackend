using Microsoft.EntityFrameworkCore;
using System;


public class UserService
{
    private FurnitureContext _context;
    public UserService(FurnitureContext context)
    {
        _context = context;
    }
    public async Task<User?> AddUser(UserDTO user)
    {
        User nuser = new User
        {
           Email = user.Email,
           Password = user.Password,
        };
        /*if (user.Authors.Any())
        {
            nuser.Authors = _context.Authors.ToList().IntersectBy(user.Authors, user => user.Id).ToList();
        }*/
        var result = _context.Users.Add(nuser);
        await _context.SaveChangesAsync();
        return await Task.FromResult(result.Entity);
    }
    public async Task<List<User>> GetUsers()
    {
        var result = await _context.Users.Include(a => a.Orders).ToListAsync();
        return await Task.FromResult(result);
    }
    public async Task<User?> GetUser(int id)
    {

        var result = _context.Users.Include(a => a.Orders).FirstOrDefault(user => user.Id == id);
        return await Task.FromResult(result);
    }
    public async Task<User?> UpdateUser(int id, UserDTO updatedUser)
    {
        var user = await _context.Users.Include(user => user.Orders).FirstOrDefaultAsync(us => us.Id == id);
        if (user != null)
        {
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;

            if (updatedUser.Orders.Any())
            {
                user.Orders = _context.Orders.ToList().IntersectBy(updatedUser.Orders, order => order.Id).ToList();
            }
            _context.Users.Update(user);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        return null;
    }
    public async Task<bool> DeleteUser(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(us => us.Id == id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}