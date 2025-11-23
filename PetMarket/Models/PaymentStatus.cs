using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetMarket.Models
{
    public class PaymentStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPaymentStatus { get; set; }
        [Required]
        [StringLength(50)]
        public required string StatusName { get; set; }
        public ICollection<Payment>? Payments { get; set; }
    }
}