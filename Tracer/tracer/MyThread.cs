using System;

namespace Tracer.tracer
{
    [Serializable]
    public class MyThread
    {
        private int _threadId;
        private long _time;
        private Method[] _methods;


        public int ThreadId
        {
            get => _threadId;
            private set => _threadId = value;
        }

        public long Time
        {
            get => _time;
            private set => _time = value;
        }
        public Method[] Methods
        {
            get => _methods;
            private set => _methods = value;
        }

//        public TYPE Type { get; set; }

        public MyThread()
        {
            
        }

        public MyThread(int id, Method[] methods)
        {
            _threadId = id;
            _time = 0;
            _methods = methods;
            for (int i = 0; i < _methods.Length; i++)
            {
                _time += methods[i].Time;
            }
        }
    }
}