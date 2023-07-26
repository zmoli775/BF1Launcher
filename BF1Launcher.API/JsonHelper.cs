namespace BF1Launcher.API;

public static class JsonHelper
{
    private static readonly JsonSerializerOptions OptionsDese = new()
    {
        IncludeFields = true,
        NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    private static readonly JsonSerializerOptions OptionsSeri = new()
    {
        WriteIndented = true,
        IncludeFields = true,
        NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };

    /// <summary>
    /// 反序列化，将json字符串转换成json类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result"></param>
    /// <returns></returns>
    public static T JsonDeserialize<T>(string result) where T : class
    {
        return JsonSerializer.Deserialize<T>(result, OptionsDese);
    }

    /// <summary>
    /// 序列化，将json类转换成json字符串
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonClass"></param>
    /// <returns></returns>
    public static string JsonSerialize<T>(T jsonClass) where T : class
    {
        return JsonSerializer.Serialize(jsonClass, OptionsSeri);
    }

    /// <summary>
    /// 读取Json文件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="savePath"></param>
    public static T ReadFile<T>(string savePath) where T : class
    {
        return JsonDeserialize<T>(File.ReadAllText(savePath));
    }

    /// <summary>
    /// 写入Json文件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="savePath"></param>
    /// <param name="jsonClass"></param>
    public static void WriteFile<T>(string savePath, T jsonClass) where T : class
    {
        File.WriteAllText(savePath, JsonSerialize(jsonClass));
    }
}
