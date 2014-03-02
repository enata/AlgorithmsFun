using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsFun.Misc
{
    public sealed class Steque2Stacks<T>
    {
        private readonly Stack<T> _stack1 = new Stack<T>(); 
        private readonly Stack<T> _stack2 = new Stack<T>(); 

        public void Push(T element)
        {
            _stack1.Push(element);
        }

        public T Pop()
        {
            if(!_stack1.Any())
                while (_stack2.Any())
                {
                    T element = _stack2.Pop();
                    _stack1.Push(element);
                }
            return _stack1.Pop();
        }

        public void Enqueue(T element)
        {
            _stack2.Push(element);
        }

        public int Count { get { return _stack1.Count + _stack2.Count; } }

        public bool Any()
        {
            return _stack1.Any() || _stack2.Any();
        }
    }
}