using System.Runtime.InteropServices;
using System.Text;

class IniFile
{
    #region DllImport
    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);
    #endregion

    public static string ReadKey(string Path, string Key, string Section = null)
    {
        var RetVal = new StringBuilder(255);
        GetPrivateProfileString(Section, Key, "", RetVal, 255, Path);
        return RetVal.ToString();
    }

    public static void WriteKey(string Path, string Key, string Value, string Section = null)
    {
        WritePrivateProfileString(Section, Key, Value, Path);
    }

    public static void DeleteKey(string Path, string Key, string Section = null)
    {
        WriteKey(Path, Key, null, Section);
    }

    public static void DeleteSection(string Path, string Section = null)
    {
        WriteKey(Path, null, null, Section);
    }

    public static bool KeyExists(string Path, string Key, string Section = null)
    {
        return ReadKey(Path, Key, Section).Length > 0;
    }
}
