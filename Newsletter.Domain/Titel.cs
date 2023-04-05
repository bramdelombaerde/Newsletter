namespace Newsletter.Domain
{
    public class Titel
    {
        public Titel(string shortName, string name)
        {
            ShortName = shortName;
            Name = name;
        }
        public Guid Id { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
    }
}