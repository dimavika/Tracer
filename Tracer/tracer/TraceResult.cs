using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Tracer.tracer
{
    public class TraceResult
    {
        private MyThread[] _threads;

        public MyThread[] Threads
        {
            get => _threads;
            set => _threads = value;
        }

        public TraceResult()
        {
        }

        public void OutPut(IOutputTracerResult outPutTracerResult, ISerializeTracerResult serializeTracerResult)
        {
            outPutTracerResult.output(serializeTracerResult.GetString(_threads));
        }

        public TraceResult(ConcurrentDictionary<int, ConcurrentStack<Method>> threads)
        {
            KeyValuePair<int, ConcurrentStack<Method>>[] pairs = threads.ToArray();
            _threads = new MyThread[pairs.Length];
            for (var i = 0; i < pairs.Length; i++)
            {
                var stack = pairs[i].Value;
                Method[] methods = stack.ToArray();
                _threads[i] = new MyThread(pairs[i].Key, methods);
            }
        }
    }
}