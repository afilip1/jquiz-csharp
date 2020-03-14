using System;
using System.Collections.Generic;
using System.Linq;

namespace JQuiz
{
    static class Extensions {
        public static T Random<T>(this IList<T> collection) {
            return collection[rng.Next(collection.Count())];
        }

        static Random rng = new Random();
    }
}
