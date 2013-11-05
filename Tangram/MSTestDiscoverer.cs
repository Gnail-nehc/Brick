using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;

namespace Tangram
{
    class MSTestDiscoverer
    {
        public string GetMSTestExeFullName()
        {

            try
            {
                string strMSTestExe = null;
                int osBit = IntPtr.Size * 8;
                RegistryKey key = null, key2 = null;
                if (osBit == 32)
                {
                    key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\VisualStudio\\10.0");
                    key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\VisualStudio\10.0\Packages\{6077292c-6751-4483-8425-9026bc0187b6}\SatelliteDll");
                }
                if (key != null && !string.IsNullOrWhiteSpace(key.GetValue("InstallDir") + ""))
                {
                    strMSTestExe = key.GetValue("InstallDir") + "MSTest.exe";
                }
                else if (key2 != null)
                {
                    strMSTestExe = (key2.GetValue("Path") + "").Replace(@"PrivateAssemblies\", "MSTest.exe");
                }
                else
                {
                    string vs100comntools = Environment.GetEnvironmentVariable("VS100COMNTOOLS");
                    if (string.IsNullOrEmpty(vs100comntools))
                    {
                        throw new Exception("can not find MSTest.exe");
                    }
                    else
                    {
                        strMSTestExe = vs100comntools.Replace("Tools\\", "") + @"IDE\mstest.exe";
                    }
                }
                if (string.IsNullOrWhiteSpace(strMSTestExe) || !File.Exists(strMSTestExe))
                {
                    throw new FileNotFoundException("Can not get the MSTest.EXE", strMSTestExe);
                }
                return strMSTestExe;
            }
            catch (Exception e)
            {
                ConsoleLog.LogError(e.Message);
                throw new RegistryException();
            }

        }
    }





    class RegistryException : Exception
    {

    }
}
