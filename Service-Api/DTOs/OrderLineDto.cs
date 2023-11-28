namespace Service_Api.DTOs
{
    public class OrderLineDto
    {
        public int Id { get; set; }
        public decimal OrderlinePrice { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int OrderlineGroupId { get; set; }

        //Empty Constructor
        public OrderLineDto() { }
    }
}
