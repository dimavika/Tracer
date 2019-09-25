using System;
using System.Threading;
using NUnit.Framework;

namespace Tracer.tracer
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            Tracer tracer = new Tracer();
            Func(tracer);
            tracer.GetTraceResult().OutPut(new ConsoleOutPut(), new JsonSerialize());
            Assert.AreEqual(1, tracer.GetTraceResult().Threads.Length);
        }
        [Test]
        public void Test2()
        {
            Tracer tracer = new Tracer();
            
            Func3(tracer);
            tracer.GetTraceResult().OutPut(new ConsoleOutPut(), new JsonSerialize());
            Assert.AreEqual(3, tracer.GetTraceResult().Threads[0].Methods[0].Methods.Length);
        }
        [Test]
        public void Test3()
        {
            Tracer tracer = new Tracer();
            Thread thread = new Thread(Func1);
            thread.Start(tracer);
            Func3(tracer);
            Thread.Sleep(100);
            tracer.GetTraceResult().OutPut(new ConsoleOutPut(), new XmlSerialize());
            Assert.AreEqual(2, tracer.GetTraceResult().Threads.Length);
        }
        
        [Test]
        public void Test4()
        {
            Tracer tracer = new Tracer();
            Func2(tracer);
            Assert.AreEqual("Tracer.tracer.Tests", tracer.GetTraceResult().Threads[0].Methods[0].ClassName);
        }

        private void Func2(Tracer tracer)
        {
            tracer.StartTrace();
            tracer.StopTrace();
        }

        private void Func1(Object tracer)
        {
            Func((Tracer) tracer);
        }

        private void Func(Tracer tracer)
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }
        private void Func3(Tracer tracer)
        {
            tracer.StartTrace();
            Func(tracer);
            Func(tracer);
            Func(tracer);
            Thread.Sleep(100);
            tracer.StopTrace();
        }
    }
}
