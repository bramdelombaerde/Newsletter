using FluentAssertions;
using Newsletter.Application.Titel;
using Newsletter.Test.Infrastructure;
using Newtonsoft.Json;
using System.Text;

namespace Newsletter.Test.Api;

public class TitelsShould : TestBase
{
    private const string TitelsUri = "titels";

    [Fact]
    public async Task Create()
    {
        //GIVEN
        await Fixture.InitializeAsync();
        var createTitel = GenerateCreateTitel();
        var json = JsonConvert.SerializeObject(createTitel);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        //WHEN
        var createResult = await Fixture.Client.PostAsync($"{TitelsUri}", data);

        //THEN
        createResult.IsSuccessStatusCode.Should().BeTrue();
        var titelCreatedResponse = await createResult.Content.ReadAsAsync<CreateTitelResponse>();

        var getResult = await Fixture.Client.GetAsync($"{TitelsUri}/{titelCreatedResponse.Id}");
        getResult.IsSuccessStatusCode.Should().BeTrue();
        var titelResponse = await getResult.Content.ReadAsAsync<TitelDetailModel>();
        titelResponse.Name.Should().Be(createTitel.Name);
        titelResponse.ShortName.Should().Be(createTitel.ShortName);
    }
}
