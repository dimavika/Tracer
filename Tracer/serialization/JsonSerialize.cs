using Newtonsoft.Json;
using Tracer.tracer;

namespace Serialization.serialization
{
    public class JsonSerialize : ISerializeTracerResult
    {
        public string GetString(MyThread[] list)
        {
            return JsonConvert.SerializeObject(list,Formatting.Indented);
        }
    }
}