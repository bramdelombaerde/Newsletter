namespace Newsletter.Domain
{
    public class Subscription : BaseEntity
    {
        public User User { get; set; }
        public Titel Titel { get; set; }
    }
}
