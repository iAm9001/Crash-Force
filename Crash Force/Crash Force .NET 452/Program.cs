using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crash_Force.NET.Core22
{
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The crash force class contains logic for initiating an intentional system crash in Windows.  This is achieved by
    /// setting the current process (this instance of the process CrashForce) to "Critical", which forces windows to crash
    /// when the critical process terminates (once the <see cref="Main(string[])"/> method has completed it's execution).
    /// This application must be executed with Admin privileges in order to successfully crash Windows.
    /// </summary>
    public class CrashForce
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetInformationProcess(
            IntPtr hProcess,
            int processInformationClass,
            ref int processInformation,
            int processInformationLength);

        /// <summary>
        /// The main method.  Once this method finishes executing, Windows will crash.  Must be executed with Admin privileges.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        static void Main(string[] args)
        {
            
            int processInformationClass = 0x1D;
            int processInformation = 1;

            Process.EnterDebugMode();

            NtSetInformationProcess(
                Process.GetCurrentProcess().Handle,
                processInformationClass,
                ref processInformation,
                sizeof(int));
        }
    }
}
