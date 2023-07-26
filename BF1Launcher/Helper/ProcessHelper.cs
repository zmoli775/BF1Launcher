namespace BF1Launcher.Helper;

public static class ProcessHelper
{
    /// <summary>
    /// 判断程序是否运行
    /// </summary>
    /// <param name="appName">程序名称</param>
    /// <returns>正在运行返回true，未运行返回false</returns>
    public static bool IsAppRun(string appName)
    {
        return Process.GetProcessesByName(appName).Length > 0;
    }

    /// <summary>
    /// 打开文件夹路径
    /// </summary>
    /// <param name="dir"></param>
    public static void OpenDir(string dir)
    {
        if (!Directory.Exists(dir))
        {
            MsgBoxHelper.Warning($"要打开的文件夹路径不存在\n{dir}");
            return;
        }

        try
        {
            Process.Start(new ProcessStartInfo(dir) { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            MsgBoxHelper.Error(ex.Message);
        }
    }

    /// <summary>
    /// 打开http链接
    /// </summary>
    /// <param name="url"></param>
    public static void OpenLink(string url)
    {
        if (!url.StartsWith("http"))
            return;

        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
    }

    /// <summary>
    /// 打开指定进程，可以附带运行参数
    /// </summary>
    /// <param name="path">本地文件路径</param>
    public static void OpenProcess(string path, string args = "")
    {
        if (!File.Exists(path))
        {
            MsgBoxHelper.Warning( $"要打开的文件路径不存在\n{path}");
            return;
        }

        try
        {
            Process.Start(path, args);
        }
        catch (Exception ex)
        {
            MsgBoxHelper.Error(ex.Message);
        }
    }

    /// <summary>
    /// 使用系统记事本打开指定文本
    /// </summary>
    /// <param name="path">本地文件路径</param>
    public static void OpenProcessWithNotepad(string path)
    {
        if (!File.Exists(path))
        {
            MsgBoxHelper.Warning($"要打开的文件路径不存在\n{path}");
            return;
        }

        try
        {
            Process.Start("notepad.exe", path);
        }
        catch (Exception ex)
        {
            MsgBoxHelper.Error(ex.Message);
        }
    }
}
