using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsFun.Misc
{
    public sealed class StaticSetOfInts
    {
        private readonly int[] _array;
        private readonly HashSet<int> _set; 

        public StaticSetOfInts(IEnumerable<int> array)
        {
            if (array == null) throw new ArgumentNullException("array");

            _array = array.OrderBy(el => el).ToArray();
            _set = new HashSet<int>(_array);
        }

        public bool Contains(int key)
        {
            return _set.Contains(key);
        }
    }
}