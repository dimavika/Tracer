using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace Tracer.tracer
{
    public class Tracer : ITracer
    {
        private ConcurrentDictionary<int, ThreadInfo> _workingThreads;

        public Tracer()
        {
            _workingThreads = new ConcurrentDictionary<int, ThreadInfo>();
        }

        public void StartTrace()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId; 
            var stack = _workingThreads.GetOrAdd(threadId, new ThreadInfo());
            
            var stackTrace = new StackTrace(true);
            var stackFrames = stackTrace.GetFrame(1);
            var methodName = stackFrames.GetMethod().Name;
            
            var classOfMethod = stackFrames.GetMethod().DeclaringType;
            var className = classOfMethod?.ToString() ?? string.Empty;
            
            var thisMethod = new Method(methodName, className);
            stack.CurrentMethods.Push(thisMethod);

            thisMethod.StartTime();
        }

        public void StopTrace()
        {
            var id = Thread.CurrentThread.ManagedThreadId; 
            var stackRun = _workingThreads.GetOrAdd(id, new ThreadInfo());
            stackRun.CurrentMethods.TryPop(out var method);
            method.StopTime();
            if (stackRun.CurrentMethods.TryPeek(out var parent)) {
                parent.AddMethod(method); 
            } else {
                stackRun.TracedMethods.Add(method);
            }
        }

        public TraceResult GetTraceResult()
        {
            return new TraceResult(_workingThreads);
        }
    }
}