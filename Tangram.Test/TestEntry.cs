using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using Tangram.UILayer.WebControls;
using Tangram.DataLayer.XmlDriven;


namespace Tangram.Test
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class TestEntry
    {
        public TestContext TestContext
        {
            get;
            set;
        }

        [TestMethod]
        public void Start()
        {
            TempReader temp = new TempReader().ReadContent();
            this.TestContext.WriteLine("Current Test Case ID: {0}", temp.TcID);
            this.TestContext.WriteLine("Current Test Case Folder: {0}", temp.TcFolder);

            string xmlpath = temp.TcFolder.Substring(temp.TcFolder.Length - 2) == "\\" ? temp.TcFolder + "DataTable.xml" : temp.TcFolder + "\\DataTable.xml";
            TestScript ts = new TestScript(temp.TcID, xmlpath, this.TestContext);
            ts.Execute();
        }
    }
}
