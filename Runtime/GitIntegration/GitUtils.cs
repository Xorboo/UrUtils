using System;
using System.Diagnostics;

namespace UrUtils.GitIntegration
{
    public static class GitUtils
    {
        public static string ExecuteGitWithParams(string param)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo("git")
                {
                    Arguments = param,
                    UseShellExecute = false,
                    WorkingDirectory = Environment.CurrentDirectory,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new Exception(process.StandardError.ReadLine());
            }

            return process.StandardOutput.ReadLine();
        }
    }
}