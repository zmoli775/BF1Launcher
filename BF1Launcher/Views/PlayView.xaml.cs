using BF1Launcher.Utils;
using BF1Launcher.Helper;

namespace BF1Launcher.Views;

/// <summary>
/// PlayView.xaml 的交互逻辑
/// </summary>
public partial class PlayView : UserControl
{
    public PlayView()
    {
        InitializeComponent();
        MainWindow.WindowClosingEvent += MainWindow_WindowClosingEvent;

        TextBox_BF1RunArgs.Text = IniHelper.ReadValue("Config", "BF1RunArgs");
    }

    private void MainWindow_WindowClosingEvent()
    {
        IniHelper.WriteValue("Config", "BF1RunArgs", TextBox_BF1RunArgs.Text.Trim());
    }

    /// <summary>
    /// 第一步 启动Origin模拟器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_RunOriginEmu_Click(object sender, RoutedEventArgs e)
    {
        ProcessHelper.OpenProcess(CoreUtil.Origin_EADesktop_exe);
    }

    /// <summary>
    /// 第二步 启动战地1游戏
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_RunBF1Game_Click(object sender, RoutedEventArgs e)
    {
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

        var args = TextBox_BF1RunArgs.Text.Trim();

        ProcessHelper.OpenProcess(CoreUtil.Game_bf1_exe, args);
    }
}
