using Newsletter.Domain.Exceptions;

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
            if (Subscriptions.Any(x => x.Titel.Id == subscription.Titel.Id))
                throw new SubscriptionException("User is already subscribed");

            Subscriptions.Add(subscription);
        }

        public void RemoveSubscription(Guid titelId)
        {
            var subscription = Subscriptions.FirstOrDefault(x => x.Titel.Id == titelId);
            if (subscription == null)
                throw new SubscriptionException("User is not subscribed");

            Subscriptions.Remove(subscription);
        }
    }
}
