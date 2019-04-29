using System;
using System.Collections.Generic;

namespace Flashcards
{
    public static class Extensions
    {
        private static Random random = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            var count = list.Count;
            while (count > 1)
            {
                count--;
                var k = random.Next(count + 1);
                var value = list[k];
                list[k] = list[count];
                list[count] = value;
            }
        }
    }
}
