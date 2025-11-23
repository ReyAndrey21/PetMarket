
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetMarket.Models
{

    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPayment { get; set; }
        public decimal Amount { get; set; }
        [Required]
        public required string PaymentMethod { get; set; } 
        public DateTime PaymentDate { get; set; }
        [ForeignKey("Order")]
        public int IdOrder { get; set; }
        public Order? Order { get; set; }
        public int IdPaymentStatus { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
    }
}