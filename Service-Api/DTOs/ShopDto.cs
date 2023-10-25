namespace Service_Api.DTOs
{
    public class ShopDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public enum _Type { Restuarant, FoodStand, CandyStore }
        public _Type Type { get; set; }

        //Empty Constructor
        public ShopDto() { }
    }
}
