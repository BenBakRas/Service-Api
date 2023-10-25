namespace Service_Api.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? ProductNumber { get; set; }
        public string? Description { get; set; }
        public double? BasePrice { get; set; }
        public int? Barcode { get; set; }
        public enum _Category { Burgere, Salater, Sides, Dips }
        public _Category Category { get; set; }
        public int? ProductGroup { get; set; }
        public String? Image { get; set; }

        public ProductDto()
        {
        }
    }
}
