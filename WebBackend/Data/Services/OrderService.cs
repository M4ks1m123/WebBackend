using Microsoft.EntityFrameworkCore;
using System;


public class OrderService
{
    private FurnitureContext _context;
    public OrderService(FurnitureContext context)
    {
        _context = context;
    }
    public async Task<Order?> AddOrder(OrderDTO order)
    {
        Order norder = new Order
        {
            PaymentId = order.PaymentId,
            User = await _context.Users.FindAsync(order.UserId),
            Product = await _context.Products.FindAsync(order.ProductId)
        };
        var result = _context.Orders.Add(norder);
        await _context.SaveChangesAsync();
        return await Task.FromResult(result.Entity);
    }
    public async Task<List<Order>> GetOrders()
    {
        var result = await _context.Orders.Include(a => a.User).Include(b => b.Product).ToListAsync();
        return await Task.FromResult(result);
    }
    public async Task<List<Order>> GetOrdersByUser(int userId)
    {
        var result = await _context.Orders.Include(a => a.User).Include(b => b.Product).Where(p => p.User.Id.Equals(userId)).ToListAsync();
        return await Task.FromResult(result);
    }
    public async Task<Order?> GetOrder(int id)
    {

        var result = _context.Orders.Include(a => a.User).Include(b => b.Product).FirstOrDefault(order => order.Id == id);

        return await Task.FromResult(result);
    }
    public async Task<Order?> UpdateOrder(int id, Order updatedOrder)
    {
        var order = await _context.Orders.Include(order => order.User).Include(b => b.Product).FirstOrDefaultAsync(or => or.Id == id);
        if (order != null)
        {
            order.PaymentId = updatedOrder.PaymentId;

            order.User = await _context.Users.FindAsync(updatedOrder.User);
            order.Product = await _context.Products.FindAsync(updatedOrder.Product);
            _context.Orders.Update(order);
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return order;
        }

        return null;
    }
    public async Task<bool> DeleteOrder(int id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(or => or.Id == id);
        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}