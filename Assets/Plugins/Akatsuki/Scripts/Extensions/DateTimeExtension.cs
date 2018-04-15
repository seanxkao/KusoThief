using System;
using UnityEngine;

public static partial class ExtensionMethods
{
    ///<summary>
    /// calculates the age based on the time passed in dateTime
    ///</summary>
    public static int GetAge(this DateTime birthdate)
    {
        var age = DateTime.Today.Year - birthdate.Year;

        // Go back to the year the person was born in case of a leap year
        return birthdate > DateTime.Today.AddYears(-age) ? --age : age;
    }

    ///<summary>
    /// saves the dateTime as a unix timestamp in playerPrefs
    ///</summary>
    public static void SaveDateToPlayerPrefs(this DateTime dateTime, string key)
    {
        var unixTimestamp = (int)dateTime.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
        PlayerPrefs.SetInt(key, unixTimestamp);
    }

    ///<summary>
    /// gets the saved unix timestamp from playerPrefs and returns as a dateTime
    ///</summary>
    public static DateTime GetDateFromPlayerPrefs(this DateTime dateTime, string key)
    {
        var timestamp = PlayerPrefs.GetInt(key);
        var now = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

        return now.AddSeconds(timestamp);
    }
}