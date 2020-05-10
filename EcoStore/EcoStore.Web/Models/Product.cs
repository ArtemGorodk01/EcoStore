namespace EcoStore.Web.Models
{
    public class Product
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public int GuaranteeMonth { get; set; }
        public string ImageData { get; set; }
        public string Description { get; set; }
    }
}
