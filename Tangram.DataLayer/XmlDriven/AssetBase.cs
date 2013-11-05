using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml.Linq;

namespace Tangram.DataLayer.XmlDriven
{
    public class AssetBase
    {
        private string id;

        public string Id
        {
            get { return id; }
        }

        private string description;

        public string Description
        {
            get { return description; }
        }

        public AssetBase(XElement element,string nodeName)
        {
            id = element.Attribute("Id").Value;
            description = element.Attribute("Description").Value;
        }

    }
}
