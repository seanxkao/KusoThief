using System;

public static partial class ExtensionMethods
{
    public static void Loop(this int count, Action<int> action)
    {
        for (int i = 0; i < count; i++)
        {
            action(i);
        }
    }
}