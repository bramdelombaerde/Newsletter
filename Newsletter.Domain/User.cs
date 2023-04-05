namespace Newsletter.Domain
{
    public class User : BaseEntity
    {
        public User(string email, string firstName, string lastName)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Subscriptions = new List<Subscription>();
        }
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Subscription> Subscriptions { get; set; }

        public void AddSubscription(Subscription subscription)
        {
            Subscriptions.Add(subscription);
        }
    }
}
