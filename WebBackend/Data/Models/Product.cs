using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(Order = 1)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public string ImgUrl { get; set; }
    public string Price { get; set; }
    public Category? Category { get; set; }
    public IEnumerable<Order> Orders { get; set; }
}