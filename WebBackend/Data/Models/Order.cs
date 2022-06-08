using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(Order = 1)]
    public int Id { get; set; }
    //public int UserId { get; set; }
    //public int ProductId { get; set; }
    public string PaymentId { get; set; }
    public User? User { get; set; }
    public Product? Product { get; set; }
    
}