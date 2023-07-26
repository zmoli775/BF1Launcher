using BF1Launcher.API.Response;

using RestSharp;

namespace BF1Launcher.API;

public static class GameTools
{
    private const string _host = "https://api.gametools.network/";

    private static readonly RestClient _client = null;

    static GameTools()
    {
        if (_client != null)
            return;

        var options = new RestClientOptions(_host)
        {
            MaxTimeout = 5000,
            ThrowOnAnyError = false,
            ThrowOnDeserializationError = false
        };

        _client = new RestClient(options);
    }

    public static async Task<Servers> GetServers(string name)
    {
        var request = new RestRequest("bf1/servers/")
            .AddHeader("accept", "application/json")
            .AddParameter("name", name)
            .AddParameter("region", "all")
            .AddParameter("platform", "pc")
            .AddParameter("limit", "10")
            .AddParameter("lang", "zh-tw");

        var response = await _client.ExecuteGetAsync(request);
        if (response.StatusCode == HttpStatusCode.OK)
            return JsonHelper.JsonDeserialize<Servers>(response.Content);

        return null;
    }
}
