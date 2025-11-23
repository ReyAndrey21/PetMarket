using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetMarket.Models
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAddress { get; set; }
        [Required]
        [StringLength(50)]
        public required string Street { get; set; } 
        [Required]
        public required int Number { get; set; } 
        [StringLength(50)]
        public string? Building { get; set; } 
        [StringLength(50)]
        public string? Apartment { get; set; } 
        [StringLength(50)]
        public string? Locality { get; set; }
        [StringLength(50)]
        public string? Village { get; set; }

        [Required]
        [StringLength(50)]
        public required string City { get; set; } 
        [Required]
        [StringLength(50)]
        public required string County { get; set; } 
        [Required]
        [StringLength(20)]
        public required string PostalCode { get; set; } 
        public ICollection<UserAddress>? UserAddress { get; set; }
        public ICollection<Order>? Order { get; set; }
    }
}
