using System;
using System.Diagnostics;

namespace Tracer.tracer
{
    [Serializable]
    public class Method
    {
        private string _name;
        private string _className;
        private long _time;
        private Stopwatch _stopwatch;
        private Method[] _methods;
        
        public string Name
        {
            get => _name;
            private set => _name = value;
        }
        public string ClassName
        {
            get => _className;
            private set => _className = value;
        }

        public Method(string name, string className)
        {
            _name = name;
            _className = className;
            _stopwatch = new Stopwatch();
            _methods = new Method[0];
        }

        public Method()
        {
            
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

        public void StartTime()
        {
            _stopwatch.Start();
        }

        public void StopTime()
        {
            _stopwatch.Stop();
            _time = _stopwatch.ElapsedMilliseconds;
        }

        public void AddMethod(Method method)
        {
            Array.Resize(ref _methods, _methods.Length+1);
            _methods[_methods.Length - 1] = method;
        }
    }
}