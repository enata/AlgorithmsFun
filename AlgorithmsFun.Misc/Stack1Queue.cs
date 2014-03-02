using System.Collections.Generic;

namespace AlgorithmsFun.Misc
{
    public sealed class Stack1Queue<T>
    {
        private readonly Queue<T> _queue = new Queue<T>();

        public void Push(T element)
        {
            _queue.Enqueue(element);
        }

        public T Pop()
        {
            for (int i = 1; i < _queue.Count; i++)
            {
                T element = _queue.Dequeue();
                _queue.Enqueue(element);
            }
            return _queue.Dequeue();
        }
    }
}