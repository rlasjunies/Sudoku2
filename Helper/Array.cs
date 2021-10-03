using System;

public static class ArrayExtensions
{
    //     public static int Push<T>(this T[] source, T value)
    //     {
    //         var index = Array.IndexOf(source, default(T));

    //         if (index != -1)
    //         {
    //             source[index] = value;
    //         }

    //         return index;
    //     }

    //     public static string StringifyInt(this int[] source)
    //     {
    //         return String.Join("", Array.ConvertAll<int, string>(source, Convert.ToString));
    //     }

    /**
The Fisher-Yates algorithms orders the array in place with a cost of of O(n) following this procedure:

Start from the last element
Randomly select one element of the array and swap it with the last element
Take the array with size decreased in one and repeat the whole process till you reach the first element.
*/
    public static T[] Shuffle_knuthfisheryates2<T>(this T[] arr)
    {
        //var temp = arr.Length;
        // var j = arr.Length;
        var lastIndex = arr.Length;
        while (lastIndex > 1)
        {
            --lastIndex;
            int randomIndex = new Random().Next(0, lastIndex + 1);
            // var rand = new Random().Next(100);
            // j = ~~(randomIndex * (i + 1));
            // Console.WriteLine($"lastIndex:{lastIndex} rand:{randomIndex}");
            var temp = arr[lastIndex];
            arr[lastIndex] = arr[randomIndex];
            arr[randomIndex] = temp;
        }

        return arr;
    }

    public static T[] ClonePush<T>(this T[] arr, T val)
    {
        T[] temp = new T[arr.Length];
        Array.Copy(arr, temp, arr.Length);
        Array.Resize(ref temp, temp.Length + 1);
        //temp[temp.Length - 1] = val;
        temp[^1] = val;
        return temp;
    }

    public static T[] Filter<T>(this T[] arr, Predicate<T> P)
    {
        T[] temp = Array.Empty<T>();
        foreach (var val in arr)
        {
            // do not push if predicate is true
            if (!P(val)) temp = temp.ClonePush<T>(val);
        }
        return temp;
    }
    public static bool Find<T>(this T[] arr, Predicate<T> P)
    {   
        // TODO to remove when test suite passed
        foreach (var val in arr) { if ( P(val) ) return true;}
        return false;
    }

}
