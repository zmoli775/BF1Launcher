using BF1Launcher.API;
using BF1Launcher.API.Response;
using BF1Launcher.Helper;
using BF1Launcher.Utils;

namespace BF1Launcher.Views;

/// <summary>
/// JoinView.xaml 的交互逻辑
/// </summary>
public partial class JoinView : UserControl
{
    public ObservableCollection<ServersItem> ServersItems { get; set; } = new();

    public JoinView()
    {
        InitializeComponent();
    }

    private async void Button_SearchServer_Click(object sender, RoutedEventArgs e)
    {
        var name = TextBox_ServerName.Text.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            MsgBoxHelper.Warning("服务器名称不能为空，请检查后重试");
            return;
        }

        ServersItems.Clear();

        Button_SearchServer.IsEnabled = false;
        TextBlock_Hints.Text = "正在搜索中...";

        var servers = await GameTools.GetServers(name);
        if (servers != null)
        {
            TextBlock_Hints.Text = "";
            servers.servers.ForEach(server => ServersItems.Add(server));
        }
        else
        {
            TextBlock_Hints.Text = "搜索失败，请重试";
        }

        Button_SearchServer.IsEnabled = true;
    }

    private async void ListBox_Servers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var index = ListBox_Servers.SelectedIndex;
        if (index == -1)
        {
            MsgBoxHelper.Warning("请先选择对应服务器");
            return;
        }

        if (!CoreUtil.IsExistsBF1GameDir())
            return;

        if (!ProcessHelper.IsAppRun("EADesktop"))
        {
            MsgBoxHelper.Warning("请先启动Origin模拟器，然后再启动战地1游戏");
            return;
        }

        if (!CoreUtil.IsExistsBF1OriginEmuPath())
        {
            MsgBoxHelper.Warning("缺少战地1免Origin补丁，请先使用战地1免Origin补丁");
            return;
        }

        ProcessHelper.CloseProcess("bf1");

        await Task.Delay(1000);

        var gameId = ServersItems[index].gameId;
        var args = $"-Client.SkipFastLevelLoad true -Online.EnableSnowroller true -VeniceOnline.EnableSnowroller true -requestState State_ConnectToGameId -gameId \"{gameId}\" -gameMode \"MP\" -role \"soldier\" -asSpectator \"false\" -parentSessinId -joinWithParty \"false\"";

        ProcessHelper.OpenProcess(CoreUtil.Game_bf1_exe, args);
    }
}
