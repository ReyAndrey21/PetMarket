using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetMarket.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdReview { get; set; }
        public required string Comment { get; set; }
        public int RatingReview { get; set; }
        public DateOnly DateReview { get; set; }
        [ForeignKey("Product")]
        public int IdProduct { get; set; }
        public Product? Product { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public User? User { get; set; }


    }
}
