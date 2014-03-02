using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsFun.Misc
{
    public sealed class Queue2Stacks<T>
    {
        private readonly Stack<T> _stack1 = new Stack<T>(); 
        private readonly Stack<T> _stack2 = new Stack<T>(); 

        public void Push(T element)
        {
            _stack1.Push(element);
        }

        public T Pop()
        {
            if(!_stack2.Any())
                while (_stack1.Any())
                {
                    var element = _stack1.Pop();
                    _stack2.Push(element);
                }
            return _stack2.Pop();
        }
    }
}