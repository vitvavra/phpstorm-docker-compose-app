using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace docker_compose
{
    class Program
    {
        static void Main(string[] args)
        {
            string argString = string.Join(" ", args);
            Regex pathReferenceReplace = new Regex(@"\\");
            Regex pathReplace = new Regex(@"C\:");

            argString = pathReferenceReplace.Replace(argString, "/");
            argString = pathReplace.Replace(argString, "/mnt/c");

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "wsl.exe",
                Arguments = "docker-compose " + argString,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                Console.WriteLine(line);
            }
            Thread.Sleep(100);

            return;
        }
    }
}
