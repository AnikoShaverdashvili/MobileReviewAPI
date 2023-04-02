using System.ComponentModel.DataAnnotations;

namespace MobileReviewAPI.Models
{
    public class Mobile
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<MobileOwner> MobileOwners { get; set; }
        public ICollection<MobileCategory> MobileCategories { get; set; }   
    }
}
