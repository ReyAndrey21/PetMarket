using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetMarket.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProduct { get; set; }
        [Required]
        public required string NameProduct { get; set; }
        [Required]
        public required string BrandProduct { get; set; }
        [Required]
        public required string DescriptionProduct { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 100000.00)]
        public required decimal PriceProduct { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public required int StockProduct { get; set; }
        public string? ImageProductUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageProduct { get; set; }
        [ForeignKey("Category")]
        public int IdCategory { get; set; }
        public Category? Category { get; set; }
        public ICollection<Review>? Review { get; set; }
        public ICollection<ShoppingCartItem>? ShoppingCartItem { get; set; }
        public ICollection<OrderDetails>? OrderDetails { get; set; }
        public ICollection<Favorites>? Favorites { get; set; }
    }
}
