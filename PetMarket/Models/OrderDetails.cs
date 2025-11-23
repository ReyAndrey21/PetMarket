using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetMarket.Models
{
    public class OrderDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOrderDetails { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtPurchase { get; set; }
        public decimal Subtotal { get; set; }
        [ForeignKey("Product")]
        public int IdProduct { get; set; }
        public Product? Product { get; set; }
        [ForeignKey("Order")]
        public int IdOrder { get; set; }
        public Order? Order { get; set; }
    }
}
