using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Tangram.DataLayer.XmlDriven
{
    public class GlobalData
    {
        private XElement globalnode;

        public GlobalData(XElement root)
        {
            globalnode = root.Element("GlobalData");
        }

        public string GetData(string nodename)
        {
            return globalnode.Element(nodename).Value;
        }
    }
}
