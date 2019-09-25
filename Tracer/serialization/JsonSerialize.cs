using Newtonsoft.Json;
using Tracer.tracer;

namespace Tracer
{
    public class JsonSerialize : ISerializeTracerResult
    {
        public string GetString(MyThread[] list)
        {
            return JsonConvert.SerializeObject(list,Formatting.Indented);
        }
    }
}