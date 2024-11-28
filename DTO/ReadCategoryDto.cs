namespace asp_net_ecommerce_web_api.DTO
{
    public class ReadCategoryDto
    {
        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}