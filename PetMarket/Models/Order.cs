using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetMarket.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOrder { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public User? User { get; set; }
        [ForeignKey("Address")]
        public int IdAddress { get; set; }
        public Address? Address { get; set; }
        public int IdPayment { get; set; }
        public Payment? Payment { get; set; }
        public ICollection<OrderDetails>? OrderDetails { get; set; }
        public ICollection<OrderStatus>? OrderStatus { get; set; }
        public ICollection<ShippingMethod>? ShippingMethod { get; set; }


    }
}
