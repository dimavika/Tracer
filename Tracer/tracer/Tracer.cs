using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace Tracer.tracer
{
    public class Tracer : ITracer
    {
        
        private ConcurrentDictionary<int, ConcurrentStack<Method>> _workingThreads;
        private ConcurrentDictionary<int, ConcurrentStack<Method>>  _stoppedThreads;
        
        public Tracer()
        {
            _workingThreads = new ConcurrentDictionary<int, ConcurrentStack<Method>>();
            _stoppedThreads = new ConcurrentDictionary<int, ConcurrentStack<Method>>();
        }

        public void StartTrace()
        {
            var thisThread = Thread.CurrentThread;
            var threadId = thisThread.ManagedThreadId;
            var stack = _workingThreads.GetOrAdd(threadId, new ConcurrentStack<Method>());
            
            var stackTrace = new StackTrace(true);
            var stackFrames = stackTrace.GetFrames();
            var stackFrame = stackFrames[1];
            var methodName = stackFrame.GetMethod().Name;
            
            var classOfMethod = stackFrame.GetMethod().DeclaringType;
            var className = classOfMethod != null ? classOfMethod.ToString() : "";
            
            var thisMethod = new Method(methodName, className);
            stack.Push(thisMethod);

            thisMethod.StartTime();
        }

        public void StopTrace()
        {
            var id = Thread.CurrentThread.ManagedThreadId; 
            var stackRun = _workingThreads.GetOrAdd(id, new ConcurrentStack<Method>());
            stackRun.TryPop(out var method);
            method.StopTime();
            var stackStop = _stoppedThreads.GetOrAdd(id, new ConcurrentStack<Method>());
            if (stackRun.TryPeek(out var parent)) {
                parent.AddMethod(method); 
            } else {
                stackStop.Push(method);
            }
        }

        public TraceResult GetTraceResult()
        {
            return new TraceResult(_stoppedThreads);
        }
    }
}