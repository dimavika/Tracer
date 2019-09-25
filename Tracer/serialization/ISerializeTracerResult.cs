using Tracer.tracer;

namespace Tracer
{
    public interface ISerializeTracerResult
    {
        string GetString(MyThread[] list);
    }
}