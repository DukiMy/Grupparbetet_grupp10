namespace HomeFinder.Models
{
    public class InterestRegistration
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public ApplicationUser User { get; set; }
    }
}
