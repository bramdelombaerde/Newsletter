namespace Newsletter.Api.Models.Newsletter
{
    public class CreateNewsletter
    {
        public Guid TitelId { get; set; }
        public Guid TemplateId { get; set; }
        public List<NewsletterToken> Tokens { get; set; } = new();
    }
}
