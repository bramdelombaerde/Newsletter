using Newsletter.Core.Clients;

namespace Newsletter.Infrastructure.Services
{
    public class ExternalClient2 : IExternalClient2
    {
        private readonly HttpClient _httpClient;

        public ExternalClient2(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<string> articles { get; set; } = new List<string>()
        {
            "Supermarktketen Delhaize gaat er alles aan doen om ervoor te zorgen dat de winkels en distributiecentra dit weekend open kunnen blijven. Dat zegt het bedrijf, nu het belangrijke paasweekend voor de deur staat. Er werden alvast deurwaarders ingezet in twee Delhaize-winkels in Aalst en Ledeberg.",
            "De UGent heeft er een nieuwe opvallende student bij: golden retriever Victor. Hij is de therapiehond van studente sociologie Els Maréchal en helpt haar om veilig en gerust naar lessen en examens te gaan. “Naar sommige proffen zit hij echt geconcentreerd te luisteren.”",
            "De broodnodige restauratie van het Groot Vleeshuis in Gent komt een stap dichterbij: het stadsbestuur heeft donderdag een bestek goedgekeurd. Ondanks alle ophef, blijft het plan gewoon om er nadien een fietsenstalling in te zetten. Eerst kunnen de Gentenaars nog eens een kijkje nemen.",
            "Opvallend overnamenieuws uit de Gentse bierwereld: het biercafé Brouwbar stopt ermee en wordt overgenomen door Dok Brewing Company."
        };

        public async Task<string> GetMostRecentArticle()
        {
            int index = new Random().Next(articles.Count - 1);
            return await Task.FromResult(articles[index]);
        }

        public async Task<bool> SendNewsletter(Domain.Newsletter newsletter) => await Task.FromResult(true);
    }
}
