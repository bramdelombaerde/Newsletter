namespace Newsletter.Domain
{
    public class Newsletter : BaseEntity
    {
        public Newsletter()
        {

        }

        public Newsletter(Titel titel, int version, string content)
        {
            Titel = titel;
            Version = version;
            Content = content;
        }
        public Guid TitelId { get; set; }
        public Titel Titel { get; set; }
        public int Version { get; set; }
        public string Content { get; set; }
        public bool IsArchived { get; set; }
    }
}
