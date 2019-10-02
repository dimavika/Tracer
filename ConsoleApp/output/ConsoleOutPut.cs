using System;
using ConsoleApp.output;

namespace ConsoleApp.output
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