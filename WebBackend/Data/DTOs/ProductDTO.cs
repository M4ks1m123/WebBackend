
public class ProductDTO
{
    public string Name { get; set; }
    public string Desc { get; set; }
    public string ImgUrl { get; set; }
    public int CategoryId { get; set; }
    public string Price { get; set; }
    public int[] orderIds { get; set; }
}