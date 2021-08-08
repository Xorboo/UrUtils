using System.Collections.Generic;
using Rand = UnityEngine.Random;

namespace UrUtils.Extensions
{
    public static class ListExtensions
    {
        public static bool Empty<T>(this IList<T> list)
        {
            return list.Count == 0;
        }

        public static T Pop<T>(this IList<T> list, int index)
        {
            T element = list[Rand.Range(0, list.Count)];
            list.RemoveAt(index);
            return element;
        }

        // Fisher-Yates shuffle
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Rand.Range(0, n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static T Random<T>(this IList<T> list) => list[Rand.Range(0, list.Count)];
        public static T RandomOrDefault<T>(this IList<T> list) => list.Empty() ? default : list[Rand.Range(0, list.Count)];
        public static T PopRandom<T>(this IList<T> list) => Pop(list, Rand.Range(0, list.Count));
        public static T PopRandomOrDefault<T>(this IList<T> list) => list.Empty() ? default : PopRandom(list);

        public static bool InRange<T>(this IList<T> list, int index) => 0 <= index && index < list.Count;
    }
}
