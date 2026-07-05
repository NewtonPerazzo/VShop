using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [Required(ErrorMessage = "The Price is required")] 
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The Description is required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "The Stock is required")]
        [Range(1, 9999)]
        public long Stock { get; set; }
        public string? ImageUrl { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }
        public string? CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
