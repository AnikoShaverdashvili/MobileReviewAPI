using MobileReviewAPI.Models;

namespace MobileReviewAPI.DTO
{
    public class ReviewerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}
