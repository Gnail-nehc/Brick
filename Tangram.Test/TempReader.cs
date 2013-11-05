using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Tangram.Test
{
    public class TempReader
    {
        public string TcID
        {
            get;
            set;
        }
        public string TcFolder
        {
            get;
            set;
        }

        public TempReader ReadContent()
        {
            string id;
            string tcFolder;
            StreamReader reader = null;
            try
            {
                string temp = Path.GetTempPath();
                reader = File.OpenText(temp + "TANGRAM.tmp");
                id = reader.ReadLine().Trim();
                tcFolder = reader.ReadLine().Trim();
                reader.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (null != reader)
                {
                    reader.Close();
                }
            }
            if (string.IsNullOrEmpty(tcFolder))
            {
                throw new Exception("TCLibPath is null or empty!!!");
            }
            return new TempReader { TcID = id, TcFolder = tcFolder };
        }






    }
}
