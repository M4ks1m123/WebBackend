using Microsoft.EntityFrameworkCore;
using System;


public class CategoryService
{
    private FurnitureContext _context;
    public CategoryService(FurnitureContext context)
    {
        _context = context;
    }
    public async Task<Category?> AddCategory(CategoryDTO category)
    {
        Category ncategory = new Category
        {
            Name = category.Name,
            Url = category.Url,
        };
        /*if (category.Authors.Any())
        {
            ncategory.Authors = _context.Authors.ToList().IntersectBy(category.Authors, category => category.Id).ToList();
        }*/
        var result = _context.Categories.Add(ncategory);
        await _context.SaveChangesAsync();
        return await Task.FromResult(result.Entity);
    }
    public async Task<List<Category>> GetCategories()
    {
        var result = await _context.Categories.Include(a => a.Products).ToListAsync();
        return await Task.FromResult(result);
    }
    public async Task<Category?> GetCategory(int id)
    {

        var result = _context.Categories.Include(a => a.Products).FirstOrDefault(category => category.Id == id);
        return await Task.FromResult(result);
    }
    public async Task<Category?> UpdateCategory(int id, Category updatedCategory)
    {
        var category = await _context.Categories.Include(category => category.Products).FirstOrDefaultAsync(au => au.Id == id);
        if (category != null)
        {
            category.Id = updatedCategory.Id;
            category.Name = updatedCategory.Name;
            category.Url = updatedCategory.Url;
            if (updatedCategory.Products.Any())
            {
                category.Products = _context.Products.ToList().IntersectBy(updatedCategory.Products, product => product).ToList();
            }
            _context.Categories.Update(category);
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }

        return null;
    }
    public async Task<bool> DeleteCategory(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(au => au.Id == id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}