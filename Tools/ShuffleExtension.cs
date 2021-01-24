using System;
using System.Collections.Generic;

namespace deckOfCards.Tools
{
    static class ShuffleExtension
    {
        private static readonly Random random = new();
        public static void Shuffle<T>(this IList<T> group)
        {
            int size = group.Count;
            while (size > 1)
            {
                size--;
                int i = random.Next(size + 1);
                T item = group[i];
                group[i] = group[size];
                group[size] = item;
            }

        }
    }
}