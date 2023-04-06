namespace Newsletter.Api.Models.NewsletterTemplate
{
    public class CreateNewsletterTemplate
    {
        public string TemplateName { get; set; }
        public Guid TitelId { get; set; }
        public string Html { get; set; }
        public List<string> Tokens { get; set; }
    }
}
