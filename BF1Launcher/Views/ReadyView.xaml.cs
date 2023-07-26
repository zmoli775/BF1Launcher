using BF1Launcher.Utils;
using BF1Launcher.Helper;

namespace BF1Launcher.Views;

/// <summary>
/// ReadyView.xaml 的交互逻辑
/// </summary>
public partial class ReadyView : UserControl
{
    public ReadyView()
    {
        InitializeComponent();
        MainWindow.WindowClosingEvent += MainWindow_WindowClosingEvent;

        TextBox_BF1GameDir.Text = IniHelper.ReadValue("Config", "BF1GameDir");
        CoreUtil.BF1_Game_Dir = TextBox_BF1GameDir.Text.Trim();
    }

    private void MainWindow_WindowClosingEvent()
    {
        IniHelper.WriteValue("Config", "BF1GameDir", TextBox_BF1GameDir.Text.Trim());
    }

    /// <summary>
    /// 选择战地1游戏所在文件夹
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_SelectBF1GameDir_Click(object sender, RoutedEventArgs e)
    {
        var fileDialog = new OpenFileDialog
        {
            Title = "选择战地1游戏所在文件夹",
            RestoreDirectory = true,
            Multiselect = false,
            Filter = "可以执行程序|*.exe",
            FileName = "bf1.exe"
        };

        if (fileDialog.ShowDialog() == true)
        {
            TextBox_BF1GameDir.Text = Path.GetDirectoryName(fileDialog.FileName);

            CoreUtil.BF1_Game_Dir = TextBox_BF1GameDir.Text.Trim();
        }
    }

    /// <summary>
    /// 使用战地1免Origin补丁
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_UseWithoutOriginPath_Click(object sender, RoutedEventArgs e)
    {
        if (!CoreUtil.IsExistsBF1GameDir())
            return;

        try
        {
            File.Copy(CoreUtil.Patch_bf1_exe, CoreUtil.Game_bf1_exe, true);
            File.Copy(CoreUtil.Patch_dinput8_dll, CoreUtil.Game_dinput8_dll, true);
            File.Copy(CoreUtil.Patch_dinput8_org_dll, CoreUtil.Game_dinput8_org_dll, true);
            File.Copy(CoreUtil.Patch_originemu_dll, CoreUtil.Game_originemu_dll, true);

            MsgBoxHelper.Information("恭喜，使用战地1免Origin补丁成功");
        }
        catch (Exception ex)
        {
            MsgBoxHelper.Exception(ex);
        }
    }

    /// <summary>
    /// 恢复战地1原版文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_UseBackupBF1MainApp_Click(object sender, RoutedEventArgs e)
    {
        if (!CoreUtil.IsExistsBF1GameDir())
            return;

        try
        {
            if (File.Exists(CoreUtil.Game_dinput8_dll))
                File.Delete(CoreUtil.Game_dinput8_dll);

            if (File.Exists(CoreUtil.Game_dinput8_org_dll))
                File.Delete(CoreUtil.Game_dinput8_org_dll);

            if (File.Exists(CoreUtil.Game_originemu_dll))
                File.Delete(CoreUtil.Game_originemu_dll);

            File.Copy(CoreUtil.Backup_bf1_exe, CoreUtil.Game_bf1_exe, true);

            MsgBoxHelper.Information("恭喜，恢复战地1原版文件成功");
        }
        catch (Exception ex)
        {
            MsgBoxHelper.Exception(ex);
        }
    }

    /// <summary>
    /// 打开数据目录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_OpenAppDataDir_Click(object sender, RoutedEventArgs e)
    {
        ProcessHelper.OpenDir(CoreUtil.Root);
    }

    /// <summary>
    /// 打开战地1游戏目录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_OpenBF1GameDir_Click(object sender, RoutedEventArgs e)
    {
        if (!CoreUtil.IsExistsBF1GameDir())
            return;

        ProcessHelper.OpenDir(CoreUtil.BF1_Game_Dir);
    }

    /// <summary>
    /// 打开战地1文档目录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_OpenBF1DocDir_Click(object sender, RoutedEventArgs e)
    {
        if (!CoreUtil.IsExistsBF1DocDir())
            return;

        ProcessHelper.OpenDir(CoreUtil.BF1_Doc_Dir);
    }

    /// <summary>
    /// 编辑Origin模拟器Cookies文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_EditOriginEmuConfig_Click(object sender, RoutedEventArgs e)
    {
        if (!CoreUtil.IsExistsBF1GameDir())
            return;

        ProcessHelper.OpenProcessWithNotepad(CoreUtil.Origin_config_ini);
    }

    /// <summary>
    /// 运行战地1繁体中文注册表修复工具
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_UseBF1RegeditFix_Click(object sender, RoutedEventArgs e)
    {
        if (MessageBox.Show("你确定要运行战地1繁体中文注册表修复工具吗？\n这个一般在修改战地1语言为繁体中文时使用",
            "注册表修复工具", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
        {
            if (!CoreUtil.IsExistsBF1GameDir())
                return;

            try
            {
                File.Copy(CoreUtil.Tools_EA_Game_RegFix_exe, CoreUtil.Game_EA_Game_RegFix_exe, true);

                ProcessHelper.OpenProcess(CoreUtil.Game_EA_Game_RegFix_exe);
            }
            catch (Exception ex)
            {
                MsgBoxHelper.Exception(ex);
            }
        }
    }
}
