using Microsoft.AspNetCore.Mvc;
using System;



[Route("api/[controller]")]
[ApiController]



public class CategoryController : ControllerBase
{
    private readonly CategoryService _context;


    public CategoryController(CategoryService context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
    {
        return await _context.GetCategories();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _context.GetCategory(id);

        if (category == null)
        {
            return NotFound();
        }

        return category;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Category>> PutCategory(int id, [FromBody] Category category)
    {
        var result = await _context.UpdateCategory(id, category);
        if (result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> PostCategory([FromBody] CategoryDTO category)
    {
        var result = await _context.AddCategory(category);
        if (result == null)
        {
            BadRequest();
        }

        return Ok(result);


    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.DeleteCategory(id);
        if (category)
        {
            return Ok();
        }
        return BadRequest();
    }

}