using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByElectionBalancer
{
    public static class Permutations
    {
        public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> list, int count)
        {
            if (count == 0)
            {
                yield return new T[0];
            }
            else
            {
                var startingElementIndex = 0;
                foreach (var startingElement in list)
                {
                    var remainingItems = list.Skip(startingElementIndex + 1);

                    foreach (var permutationOfRemainder in Permute(remainingItems, count - 1))
                    {
                        yield return Concat(new[] { startingElement }, permutationOfRemainder);
                    }
                    startingElementIndex += 1;
                }
            }
        }

        public static IEnumerable<T> Concat<T>(IEnumerable<T> a, IEnumerable<T> b)
        {
            foreach (var item in a)
            {
                yield return item;
            }
            foreach (var item in b)
            {
                yield return item;
            }
        }
    }
}
