using System;

namespace Tracer
{
    public class ConsoleOutPut : IOutputTracerResult
    {
        public  ConsoleOutPut(){}
        public void output(string result)
        {
            Console.WriteLine(result);
        }
    }
}