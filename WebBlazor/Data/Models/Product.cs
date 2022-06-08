namespace WebBlazor.Data.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public string ImgUrl { get; set; }
    public string CategoryId { get; set; }
    public string Price { get; set; }
    public IEnumerable<Order> Orders { get; set; }
}