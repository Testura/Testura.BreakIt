using System.IO;

namespace Testura.BreakIt.TestValues.TestValueLoggers
{
    public class FileTestValueLogger : TestValueLogger
    {
        private readonly string _path;

        public FileTestValueLogger(string path)
        {
            _path = path;
        }

        public override void Log(string message)
        {
            using (var file = File.AppendText(_path))
            {
                file.WriteLine(message);
            }
        }
    }
}
