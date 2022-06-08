using Microsoft.AspNetCore.Mvc;
using System;


[Route("api/[controller]")]
[ApiController]

public class ProductController : ControllerBase
{
    private readonly ProductService _context;

    public ProductController(ProductService context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
    {
        return await _context.GetProducts();
    }

    [HttpGet("category/{categoryUrl}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string categoryUrl)
    {
        return await _context.GetProductsByCategory(categoryUrl);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.GetProduct(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> PutProduct(int id, [FromBody] Product product)
    {
        var result = await _context.UpdateProduct(id, product);
        if (result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct([FromBody] ProductDTO product)
    {
        var result = await _context.AddProduct(product);
        if (result == null)
        {
            BadRequest();
        }

        return Ok(result);


    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.DeleteProduct(id);
        if (product)
        {
            return Ok();
        }
        return BadRequest();
    }


}