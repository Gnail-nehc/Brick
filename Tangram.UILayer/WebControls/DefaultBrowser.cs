using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace Tangram.UILayer.WebControls
{
    public class DefaultBrowser : BrowserWindow
    {
        public bool Maximized
        {
            get {   return base.Maximized;  }
            set { base.Maximized = value; }
        }

        public DefaultBrowser()
        {
            #region Search Criteria
            this.SearchProperties[UITestControl.PropertyNames.ClassName] = "IEFrame";
            #endregion
        }

        public void CloseBrowser()
        {
            System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process proc in procs)
            {
                if (proc.ProcessName.ToUpper() == "IEXPLORE")
                {
                    proc.Kill();
                }
            }
        }

        public void LaunchUrl(string uriString)
        {
            System.Uri uri = new Uri(uriString);
            this.CopyFrom(BrowserWindow.Launch(uri));
            this.WaitForControlReady();
        }


        //public void WaitForControlReady(int second)
        //{
        //    this.WaitForControlReady(second*1000);
        //}
    }
}
