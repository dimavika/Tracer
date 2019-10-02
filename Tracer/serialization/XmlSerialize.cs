using System.IO;
using System.Xml.Serialization;
using Tracer.tracer;

namespace Serialization.serialization
{
    public class XmlSerialize : ISerializeTracerResult
    {
        public  XmlSerialize(){}
        public string GetString(MyThread[] list)
        {
            StringWriter stream = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(MyThread[]));
            serializer.Serialize(stream,list);
            return  stream.ToString();
        }
    }
}