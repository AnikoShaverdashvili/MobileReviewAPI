using System.ComponentModel.DataAnnotations;

namespace MobileReviewAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<MobileCategory> MobileCategories { get; set; }

    }
}
