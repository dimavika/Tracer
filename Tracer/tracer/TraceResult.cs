using System.Collections.Concurrent;
using System.Collections.Generic;
using ConsoleApp.output;
using Serialization.serialization;

namespace Tracer.tracer
{
    public class TraceResult
    {
        private MyThread[] _threads;

        public MyThread[] Threads 
        {
            get => _threads;
            private set => _threads = value;
        }

        public TraceResult()
        {
        }

        public void OutPut(IOutputTracerResult outPutTracerResult, ISerializeTracerResult serializeTracerResult)
        {
            outPutTracerResult.output(serializeTracerResult.GetString(_threads));
        }

        public TraceResult(ConcurrentDictionary<int, ThreadInfo> threads)
        {
            KeyValuePair<int, ThreadInfo>[] pairs = threads.ToArray();
            _threads = new MyThread[pairs.Length]; 
            for (var i = 0; i < pairs.Length; i++)
            {
                var stack = pairs[i].Value;
                Method[] methods = stack.TracedMethods.ToArray();
                _threads[i] = new MyThread(pairs[i].Key, methods);
            }
        }
    }
}