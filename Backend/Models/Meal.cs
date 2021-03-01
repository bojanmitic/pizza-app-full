namespace pizza_server.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int MealSectionId { get; set; }
    }
}
