namespace Newsletter.Api.Models.Newsletter
{
    public class NewsletterToken
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public NewsletterTokenSource Source { get; set; }
    }
}
