using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileReviewAPI.Models
{
    public class Country
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = ("nvarchar(255)"))]
        public string Name { get; set; }
        public ICollection<Owner> Owners { get; set; }
        

    }
}
