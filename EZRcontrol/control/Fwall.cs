using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace EZRcontrol
{
    //http://crystalcube.co.kr/12
    public static class Fwall
    {
        private static readonly string FirewallCmd = "netsh firewall add allowedprogram \"{1}\" \"{0}\" ENABLE";
        private static readonly string AdvanceFirewallCmd = "netsh advfirewall firewall add rule name=\"{0}\" dir=in action=allow program=\"{1}\" enable=yes";
        private static readonly int VistaMajorVersion = 6;

        public static bool AuthorizeProgram(string name, string programFullPath)
        {
            try
            { // OS version check
                string strFormat = Fwall.FirewallCmd;
                if (System.Environment.OSVersion.Version.Major >= Fwall.VistaMajorVersion)
                {
                    strFormat = Fwall.AdvanceFirewallCmd;
                }

                // Start to register 
                string command = String.Format(strFormat, name, programFullPath);
                System.Console.WriteLine(command);

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = true;
                startInfo.FileName = "cmd.exe";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardInput = true;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;

                Process process = new Process();
                process.EnableRaisingEvents = false;
                process.StartInfo = startInfo;
                process.Start();
                process.StandardInput.Write(command + Environment.NewLine);
                process.StandardInput.Close();
                string result = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();
                process.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
