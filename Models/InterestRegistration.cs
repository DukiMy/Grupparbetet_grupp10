namespace HomeFinder.Models
{
    public class InterestRegistration
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        ApplicationUser User { get; set; }
    }
}
