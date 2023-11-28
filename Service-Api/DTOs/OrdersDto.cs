namespace Service_Api.DTOs
{
    public class OrdersDto
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime DateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public int ShopId { get; set; }
        //Empty Constructor
        public OrdersDto() { }

    }
}
