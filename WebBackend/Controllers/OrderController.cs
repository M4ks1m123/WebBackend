﻿using Microsoft.AspNetCore.Mvc;
using System;


[Route("api/[controller]")]
[ApiController]

public class OrderController : ControllerBase
{
    private readonly OrderService _context;

    public OrderController(OrderService context)
    {
        _context = context;
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
    {
        return await _context.GetOrders();
    }

    [HttpGet("shopping-cart/{id}")]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUser(int id)
    {
        return await _context.GetOrdersByUser(id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await _context.GetOrder(id);

        if (order == null)
        {
            return NotFound();
        }

        return order;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Order>> PutOrder(int id, [FromBody] Order order)
    {
        var result = await _context.UpdateOrder(id, order);
        if (result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Order>> PostOrder([FromBody] OrderDTO order)
    {
        var result = await _context.AddOrder(order);
        if (result == null)
        {
            BadRequest();
        }

        return Ok(result);


    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _context.DeleteOrder(id);
        if (order)
        {
            return Ok();
        }
        return BadRequest();
    }


}