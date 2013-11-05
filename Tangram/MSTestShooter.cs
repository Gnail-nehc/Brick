using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;

namespace Tangram
{
    class MSTestShooter
    {
        private const string TEST_METHOD_NAME = "Start";
        public int CallMsTest(string tcId, string msTestExeFullName, string testAssemblyFullName, string reportFullName, string testCaseFolder)
        {
            try
            {

                TempRecorder temp = new TempRecorder();
                temp.WriteContent(tcId, testCaseFolder);
                ProcessStartInfo pStartInfo = new ProcessStartInfo(msTestExeFullName);
                pStartInfo.CreateNoWindow = false;
                pStartInfo.UseShellExecute = false;

                pStartInfo.Arguments = string.Format("/nologo /testcontainer:\"{0}\" /test:{1} /resultsfile:\"{2}\"",
                    testAssemblyFullName,
                    TEST_METHOD_NAME,
                    reportFullName);

                ConsoleLog.LogDebug("Process Arguments: {0}", pStartInfo.Arguments);

                try
                {
                    ConsoleLog.LogInfo(msTestExeFullName);
                    using (Process p = Process.Start(pStartInfo))
                    {
                        if (p != null)
                        {
                            ConsoleLog.LogDebug("Current Process ID: {0}", p.Id);
                        }
                        p.WaitForExit();
                        return p.ExitCode;
                    }
                }
                catch (Win32Exception e)
                {
                    ConsoleLog.LogError("class:MSTestShooter, method:CallMsTest:{0}", e);
                    return ExitCode.ProcessStartException;
                }
            }
            catch (Exception e)
            {
                ConsoleLog.LogError("unknown exception:{0}", e);
                return ExitCode.UnknownPlaybackException;
            }
        }
    }


    sealed class ExitCode
    {
        public const int Success = 0;
        public const int UnknownException = 2;
        public const int UnknownPlaybackException = 3;
        public const int TCInvalid = 4;
        public const int RegistryException = 5;
        public const int ProcessStartException = 6;
    }
}
