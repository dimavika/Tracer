using System.IO;

namespace ConsoleApp.output
{
    public class FileOutPut : IOutputTracerResult
        {
            private static  string _path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            private const string Directory = @"/dimavi_ka/RiderProjects/Tracer/Tracer";
            public void output(string result)
            {
                string name =@"result"+".txt";
                using (FileStream fstream = new FileStream(_path.Substring(0, _path.LastIndexOf(@"\Tracer")+8)+Directory+name, FileMode.OpenOrCreate))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(result);
                    fstream.Write(array, 0, array.Length);
                }
            }
        }
    }
