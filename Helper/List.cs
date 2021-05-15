using System;
using System.Collections.Generic;

public static class ListExtensions
{

    /**
The Fisher-Yates algorithms orders the array in place with a cost of of O(n) following this procedure:

Start from the last element
Randomly select one element of the array and swap it with the last element
Take the array with size decreased in one and repeat the whole process till you reach the first element.
*/
    public static List<T> Shuffle_knuthfisheryates2<T>(this List<T> list)
    {
        // var temp = list.Length;
        // var j = arr.Length;
        var lastIndex = list.Count;
        while (lastIndex > 1)
        {
            --lastIndex;
            int randomIndex = new Random().Next(0, lastIndex + 1);
            // var rand = new Random().Next(100);
            // j = ~~(randomIndex * (i + 1));
            // Console.WriteLine($"lastIndex:{lastIndex} rand:{randomIndex}");
            var temp = list[lastIndex];
            list[lastIndex] = list[randomIndex];
            list[randomIndex] = temp;
        }

        return list;
    }
}