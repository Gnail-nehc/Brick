using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Xml.Linq;

namespace Tangram
{
    class Program
    {
        private const string CODEDUI_LIBRARY = "Tangram.Test.dll";

        static void Main(string[] args)
        {
            string exeFolder = AppDomain.CurrentDomain.BaseDirectory;
            ConsoleLog.LogDebug("EXE file's Folder:" + exeFolder);
            string strTestFileFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CODEDUI_LIBRARY);
            try
            {
                Environment.SetEnvironmentVariable("EexFolder", exeFolder, EnvironmentVariableTarget.User);
                MSTestShooter executioner = new MSTestShooter();
                MSTestDiscoverer mst = new MSTestDiscoverer();
                string strMSTestExeFullName = mst.GetMSTestExeFullName();
                if(args.Length == 0)//in dev mode
                {
                    string strDataFileFullName = AppDomain.CurrentDomain.BaseDirectory + "DataTable.xml";
                    string strTestReportFolder = XElement.Load(strDataFileFullName).Element("GlobalData").Element("LogPath").Value;
                    if (!Directory.Exists(strTestReportFolder))
                    {
                        Directory.CreateDirectory(strTestReportFolder);
                    }
                    INPUT: Console.Write("Please input test case ID:");
                    int tcid;
                    string id = Console.ReadLine().ToString();
                    string strReportFullName = string.Empty;
                    if (int.TryParse(id,out tcid))
                    {
                        strReportFullName = strTestReportFolder + "\\TC" + id + "_" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".trx";

                        ConsoleLog.LogDebug("Test case {0} is running ...", id);

                        if (executioner.CallMsTest(id, strMSTestExeFullName, strTestFileFullName, strReportFullName, AppDomain.CurrentDomain.BaseDirectory) == 0)
                        {
                            ConsoleLog.LogInfo("{0} PASS!", id);
                        }
                        else
                        {
                            ConsoleLog.LogError("{0} FAILED!", id);
                        }
                    }
                    else
                    {
                        goto INPUT;
                    }
                }
                else
                {
                    string strTestDataFile = args[1];//DataTable.xml
                    string strReportFullName = args[2];
                    string strTestCaseFolder = args[3];
                    int tcId;
                    string id=args[0];
                    if (int.TryParse(args[0], out tcId))
                    {
                        int exitCode = executioner.CallMsTest(id, strMSTestExeFullName, strTestFileFullName, strReportFullName, strTestCaseFolder);
                        Environment.Exit(exitCode);
                    }
                    else
                    {
                        Console.WriteLine("Test case ID is invalid.");
                        Environment.Exit(ExitCode.TCInvalid);
                    }
                }
                Console.Write("Done!");
                Console.Read();
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
            }
        }
    }
}
