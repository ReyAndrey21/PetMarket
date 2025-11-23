using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetMarket.Models
{
    public class User : IdentityUser
    {
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        [NotMapped]
        public IFormFile? ProfilePicture { get; set; }  

        public DateTime CreatedAt { get; set; }

        public ShoppingCart? ShoppingCart { get; set; }
        public ICollection<UserAddress>? UserAddress { get; set; }
        public ICollection<Order>? Order { get; set; }
        public ICollection<Review>? Review { get; set; }
        public ICollection<Favorites>? Favorites { get; set; }
        

    }
}
