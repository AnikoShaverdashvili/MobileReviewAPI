namespace MobileReviewAPI.Models
{
    public class MobileCategory
    {
        public int MobileId { get; set; }
        public int CategoryId { get;set; }
        public Mobile Mobile { get; set; }
        public Category Category { get; set; }
    }
}
