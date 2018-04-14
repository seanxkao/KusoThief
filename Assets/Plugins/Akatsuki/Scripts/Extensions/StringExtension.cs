using System;

public static partial class ExtensionMethods
{
    public static string PadLeftWithZero(this string s, int length)
    {
        s = s.PadLeft(length, '0');
        s = s.Substring(s.Length - length, length);
        return s;
    }

    public static string ToTitleUpper(this string s)
    {
        if (s == null)
        {
            return null;
        }

        if (s.Length > 1)
        {
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        return s.ToUpper();
    }

    public static string ToTitleUpper(this string s, char splitter)
    {
        if (s == null)
        {
            return null;
        }

        var strings = s.Split(splitter);

        var returnStr = string.Empty;
        foreach (var str in strings)
        {
            returnStr += char.ToUpper(str[0]) + str.Substring(1);
        }

        return returnStr;
    }

    ///<summary>
    /// converts the string using format. on failure, will convert to DateTime.Now.
    ///</summary>
    public static DateTime ToDateTime(this string s, string format)
    {
        DateTime result;

        DateTime.TryParseExact(s, format, null, System.Globalization.DateTimeStyles.None, out result);

        return result;
    }

    ///<summary>
    /// checks if string exactly matches the "date" format provided
    ///</summary>
    public static bool IsInDateFormat(this string s, string format)
    {
        DateTime result;

        return DateTime.TryParseExact(s, format, null, System.Globalization.DateTimeStyles.None, out result);
    }
}
