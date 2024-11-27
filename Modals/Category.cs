namespace asp_net_ecommerce_web_api.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}