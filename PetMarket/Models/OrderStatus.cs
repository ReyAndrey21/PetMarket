using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetMarket.Models
{
    public class OrderStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOrderStatus { get; set; }
        public required string Status { get; set; }
        public DateTime UpdatedAt { get; set; }
        [ForeignKey("Order")]
        public int IdOrder { get; set; }
        public Order? Order { get; set; }
    }
}
