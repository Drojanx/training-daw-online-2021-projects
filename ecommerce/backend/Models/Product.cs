
namespace backend.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Short { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public float Price { get; set; }

        public string MainImage { get; set; } = string.Empty;

        public int  Disccount { get; set; }
        
        public string Images { get; set; } = string.Empty;
                

    }
}
