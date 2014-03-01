using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsFun.Misc
{
    public sealed class Sum4Checker
    {
        /// <summary>
        ///     Find number of 4-tuples that sums to 0
        /// </summary>
        public int Find(int[] arr)
        {
            if (arr == null) throw new ArgumentNullException();

            if (arr.Length < 4) return 0;

            ValueHolder[] valueHolders = arr.Select(num => new ValueHolder(num))
                                            .ToArray();
            Dictionary<int, List<Tuple<ValueHolder, ValueHolder>>> sums2 = BuildSums2Dictionary(valueHolders);
            
            var result = Find4Tuples(sums2);
            return result.Count;
        }

        private static HashSet<HashSet<ValueHolder>> Find4Tuples(Dictionary<int, List<Tuple<ValueHolder, ValueHolder>>> sums2)
        {
            var observed = new HashSet<int>();
            var result = new HashSet<HashSet<ValueHolder>>(new HashSetComparer());
            foreach (var sum2 in sums2)
            {
                int opposite = -sum2.Key;
                if (observed.Contains(sum2.Key) || !sums2.ContainsKey(opposite))
                    continue;

                foreach (var oppSum in sums2[opposite])
                    foreach (var sumOpperands in sum2.Value)
                    {
                        if (oppSum.Item1 != sumOpperands.Item1 && oppSum.Item1 != sumOpperands.Item2 &&
                            oppSum.Item2 != sumOpperands.Item1 && oppSum.Item2 != sumOpperands.Item2)
                            result.Add(new HashSet<ValueHolder>
                                {
                                    oppSum.Item1,
                                    oppSum.Item2,
                                    sumOpperands.Item1,
                                    sumOpperands.Item2
                                });
                    }
                observed.Add(opposite);
            }
            return result;
        }

        private sealed class HashSetComparer: IEqualityComparer<HashSet<ValueHolder>> 
        {

            public bool Equals(HashSet<ValueHolder> x, HashSet<ValueHolder> y)
            {
                return x.SetEquals(y);
            }

            public int GetHashCode(HashSet<ValueHolder> obj)
            {
                var hash = obj.Select(o => o.GetHashCode())
                              .OrderBy(o => o)
                              .Aggregate(0, (i, i1) => i ^ 397 + i1);
                return hash;
            }
        }

        private static Dictionary<int, List<Tuple<ValueHolder, ValueHolder>>> BuildSums2Dictionary(ValueHolder[] arr)
        {
            var sums2 = new Dictionary<int, List<Tuple<ValueHolder, ValueHolder>>>();
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    int sum = arr[i].Value + arr[j].Value;
                    if (!sums2.ContainsKey(sum))
                        sums2[sum] = new List<Tuple<ValueHolder, ValueHolder>>();
                    sums2[sum].Add(new Tuple<ValueHolder, ValueHolder>(arr[i], arr[j]));
                }
            }
            return sums2;
        }

        private sealed class ValueHolder
        {
            public readonly int Value;

            public ValueHolder(int value)
            {
                Value = value;
            }
        }
    }
}