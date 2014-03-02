using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsFun.Misc
{
    public sealed class DequeWithStackSteque<T>
    {
        private readonly Stack<RefWrapper> _stack = new Stack<RefWrapper>(); 
        private readonly Steque2Stacks<RefWrapper> _steque = new Steque2Stacks<RefWrapper>();
        private int _count;
 
        public void EnqueueFront(T element)
        {
            _steque.Push(new RefWrapper(element));
            _count++;
        }

        public void EnqueueBack(T element)
        {
            var elementWrapper = new RefWrapper(element);
            _steque.Enqueue(elementWrapper);
            _stack.Push(elementWrapper);
            _count++;
        }

        public T DequeueFront()
        {
            if(_count == 0)
                throw new InvalidOperationException();

            if (!_steque.Any())
            {
                RefillSteque();
            }

            return PopFromSteque();
        }

        public T DequeueBack()
        {
            if (_count == 0)
                throw new InvalidOperationException();

            if (!_stack.Any())
            {
                RefillStack();
            }
            return PopFromStack();
        }

        private T PopFromStack()
        {
            var elementWrapper = _stack.Pop();
            while (elementWrapper.Deleted)
            {
                elementWrapper = _stack.Pop();
            }
            var value = elementWrapper.Value;
            elementWrapper.Deleted = true;
            return value;
        }

        private void RefillStack()
        {
            for (int i = 0; i < _steque.Count; i++)
            {
                var elementWrapper = _steque.Pop();
                if (!elementWrapper.Deleted)
                {
                    _stack.Push(elementWrapper);
                    _steque.Enqueue(elementWrapper);
                }
            }
        }

        private T PopFromSteque()
        {
            RefWrapper wrappedElement = _steque.Pop();
            while (wrappedElement.Deleted)
            {
                wrappedElement = _steque.Pop();
            }
            var value = wrappedElement.Value;
            wrappedElement.Deleted = true;
            return value;
        }

        private void RefillSteque()
        {
            while (_stack.Any())
            {
                RefWrapper wrappedElement = _stack.Pop();
                if (!wrappedElement.Deleted)
                    _steque.Push(wrappedElement);
            }
        }

        

        private sealed class RefWrapper
        {
            public readonly T Value;
            public bool Deleted;

            public RefWrapper(T value)
            {
                Value = value;
            }
        }
    }
}