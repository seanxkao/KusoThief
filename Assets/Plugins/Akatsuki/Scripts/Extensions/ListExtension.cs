using System.IO;
using System.Collections.Generic;

public static partial class ExtensionMethods
{
    public static void Swap<T>(this List<T> list, 
                                   int index1, 
                                   int index2)
    {
        var temp = list[index1];
        list[index1] = list[index2];
        list[index2] = temp;        
    }
}
