using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Tangram
{
    class TempRecorder
    {
        public void WriteContent(string tcId, string testCaseFullName)
        {
            if (string.IsNullOrEmpty(testCaseFullName))
            {
                throw new Exception("Test Case's path is null or empty!!!!");
            }
            string temp = Path.GetTempPath();
            FileStream s = null;
            StreamWriter writer = null;
            try
            {
                s = File.Open(temp + "TANGRAM.tmp", FileMode.Create);
                writer = new StreamWriter(s);
                writer.WriteLine(tcId);
                writer.WriteLine(testCaseFullName);
            }
            catch (Exception e)
            {
                throw new Exception("Create axisuia.tmp failed ---->" + e.Message);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
                if (s != null)
                {
                    s.Close();
                }
            }
        }
    }
}
