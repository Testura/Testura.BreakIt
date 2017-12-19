using System.IO;

namespace Testura.ApiTester.Combinations.Loggers
{
    public class FileCombinationLogger : CombinationLogger
    {
        private readonly string _path;

        public FileCombinationLogger(string path)
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
