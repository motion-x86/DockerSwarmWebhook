using System.Diagnostics;

namespace WebHooks.Jobs
{
    public static class ShellExecute
    {
        public static string ExecuteCommand(this string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            using (var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            })
            {
                process.Start();
                string result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                return result;
            }
        }
    }
}
