using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Tracer.tracer
{
    public class ThreadInfo
    {

        public ThreadInfo()
        {
            CurrentMethods = new ConcurrentStack<Method>();
            TracedMethods = new List<Method>();
        }

        public  ConcurrentStack<Method> CurrentMethods { get; private set; }

        public List<Method> TracedMethods { get; private set; }
    }
}