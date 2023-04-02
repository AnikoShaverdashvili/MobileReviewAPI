namespace MobileReviewAPI.Models
{
    public class MobileOwner
    {
        public int MobileId { get; set; }
        public int OwnerId { get; set; }
        public Mobile Mobile { get; set; }  
        public Owner Owner { get; set; }
    }
}
