namespace Newsletter.Domain
{
    public class NewsletterTemplate : BaseEntity
    {
        public string TemplateName { get; set; }
        public Titel Titel { get; set; }
        public string Html { get; set; }
        public List<string> Tokens { get; set; }
    }
}
