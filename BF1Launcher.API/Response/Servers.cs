namespace BF1Launcher.API.Response;

public class Servers
{
    public List<ServersItem> servers { get; set; }
}

public class ServersItem
{
    public string prefix { get; set; }
    public string description { get; set; }
    public int playerAmount { get; set; }
    public int maxPlayers { get; set; }
    public int inSpectator { get; set; }
    public int inQue { get; set; }
    public string serverInfo { get; set; }
    public string url { get; set; }
    public string mode { get; set; }
    public string currentMap { get; set; }
    public string ownerId { get; set; }
    public string region { get; set; }
    public string platform { get; set; }
    public string serverId { get; set; }
    public bool isCustom { get; set; }
    public string smallMode { get; set; }
    [JsonIgnore]
    public Teams teams { get; set; }
    public bool official { get; set; }
    public string gameId { get; set; }
}

public class Teams
{
}
