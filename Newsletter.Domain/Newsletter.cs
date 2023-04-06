namespace Newsletter.Domain
{
    public class Newsletter : BaseEntity
    {
        public Titel Titel { get; set; }
        public int Version { get; set; }
        public string Html { get; set; }
        public bool IsArchived { get; set; }
        public List<NewsletterToken> Tokens { get; set; }
    }
}
