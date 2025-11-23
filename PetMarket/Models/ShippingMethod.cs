using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetMarket.Models
{
    public class ShippingMethod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdShippingMethod { get; set; }
        public required string MethodName { get; set; }
        public required string Description { get; set; }
        public decimal Cost { get; set; }
        public int FreeShippingThreshold { get; set; }
        [ForeignKey("Order")]
        public int IdOrder { get; set; }
        public Order? Order { get; set; }

    }
}
