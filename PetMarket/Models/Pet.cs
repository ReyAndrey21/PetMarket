using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetMarket.Models
{
    public class Pet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPet { get; set; }
        public required string TypePet { get; set; }
        public ICollection<Category>? Category { get; set; }


    }
}
