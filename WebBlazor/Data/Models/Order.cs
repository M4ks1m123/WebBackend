namespace WebBlazor.Data.Models;

public class Order
{

    public int Id { get; set; }
    public string PaymentId { get; set; }
    public User? User { get; set; }
    public Product? Product { get; set; }

}