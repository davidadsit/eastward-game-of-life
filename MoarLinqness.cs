using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public static class MoarLinqness
    {
        public static bool None<T>(this IEnumerable<T> thing)
        {
            return !thing.Any();
        }
    }
}