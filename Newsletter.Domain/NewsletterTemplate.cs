namespace Newsletter.Domain
{
    public class NewsletterTemplate : BaseEntity
    {
        public NewsletterTemplate(string templateName, Titel titel, string html, List<string> tokens)
        {
            TemplateName = templateName;
            Titel = titel;
            Html = html;
            Tokens = tokens;
        }
        public string TemplateName { get; set; }
        public Titel Titel { get; set; }
        public string Html { get; set; }
        public List<string> Tokens { get; set; }
    }
}
