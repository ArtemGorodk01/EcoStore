namespace EcoStore.Web.Models
{
    public class UserReview
    {
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public int Mark { get; set; }
        public string Review { get; set; }
    }
}
