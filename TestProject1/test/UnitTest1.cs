using System;
using System.Threading;
using ConsoleApp.output;
using NUnit.Framework;
using Serialization.serialization;

namespace TestProject1.test
{
    
        [TestFixture]
        public class Tests
        {

            [Test]
            public void ShouldBeOneThread()
            {
                Tracer.tracer.Tracer tracer = new Tracer.tracer.Tracer();
                Func(tracer: tracer);
                tracer.GetTraceResult().OutPut(new ConsoleOutPut(), new JsonSerialize());
                Assert.AreEqual(1, tracer.GetTraceResult().Threads.Length);
            }

            [Test]
            public void ShouldBeThreeMethods()
            {
                Tracer.tracer.Tracer tracer = new Tracer.tracer.Tracer();

                Func3(tracer);
                tracer.GetTraceResult().OutPut(new ConsoleOutPut(), new JsonSerialize());
                Assert.AreEqual(3, tracer.GetTraceResult().Threads[0].Methods[0].Methods.Length);
            }

            [Test]
            public void ShouldBeTwoThreads()
            {
                Tracer.tracer.Tracer tracer = new Tracer.tracer.Tracer();
                Thread thread = new Thread(() => Func1(tracer));
                thread.Start();
                Func3(tracer);
                Func(tracer);
                Thread.Sleep(100);
                tracer.GetTraceResult().OutPut(new ConsoleOutPut(), new JsonSerialize());
                Assert.AreEqual(2, tracer.GetTraceResult().Threads.Length);
            }

            [Test]
            public void ShouldBeTestsName()
            {
                Tracer.tracer.Tracer tracer = new Tracer.tracer.Tracer();
                Func2(tracer);
                Assert.AreEqual("TestProject1.test.Tests", tracer.GetTraceResult().Threads[0].Methods[0].ClassName);
            }

            private void Func2(Tracer.tracer.Tracer tracer)
            {
                tracer.StartTrace();
                tracer.StopTrace();
            }

            private void Func1(Object tracer)
            {
                Func((Tracer.tracer.Tracer) tracer);
            }

            private void Func(Tracer.tracer.Tracer tracer)
            {
                tracer.StartTrace();
                Thread.Sleep(100);
                tracer.StopTrace();
            }

            private void Func3(Tracer.tracer.Tracer tracer)
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
