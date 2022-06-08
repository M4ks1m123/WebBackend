using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(Order = 1)]

    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public IEnumerable<Product> Products { get; set; }
}
