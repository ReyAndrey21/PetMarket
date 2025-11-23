using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetMarket.Models
{
    public class ShoppingCartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdShoppingCartItem { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtAdd { get; set; }
        [ForeignKey("Product")]
        public int IdProduct { get; set; }
        public Product? Product { get; set; }
        [ForeignKey("ShoppingCart")]
        public int IdShoppingCart { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
    }
}
