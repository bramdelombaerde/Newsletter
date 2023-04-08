namespace Newsletter.Domain
{
    public class Titel : BaseEntity
    {
        public Titel(string shortName, string name)
        {
            ShortName = shortName;
            Name = name;
        }
        public Guid Id { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public List<Subscription> Subscriptions { get; set; }
        public List<NewsletterTemplate> NewsletterTemplates { get; set; }
        public List<Newsletter> Newsletters { get; set; }
    }
}