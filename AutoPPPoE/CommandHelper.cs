using System;
using System.Diagnostics;

namespace AutoPPPoE
{
    public static class CommandHelper
    {
        public static string runCommand(string command)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

                string stdOut;
                try
                {
                    process.Start();
                    process.StandardInput.WriteLine(command);
                    process.StandardInput.WriteLine("exit");

                    stdOut = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                }
                catch (Exception ex)
                {
                    stdOut = ex.Message;
                }
                return stdOut;
            }
        }
    }
}