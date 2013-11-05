using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml.Linq;

namespace Tangram.DataLayer.XmlDriven
{
    public class Scenario:AssetBase
    {
        private Dictionary<string, object> dictElements = new Dictionary<string, object>();

        public Dictionary<string, object> Operations
        {
            get { return dictElements; }
        }


        public Scenario(XElement element, XElement root)
            : base(element, "Scenario")
        {
            int counter = 1;
            foreach (XElement e in element.Elements())
            {
                string nodeName = e.Name.ToString();
                if (nodeName.IndexOf("Action") > -1)
                {
                    string actionId = e.Value;
                    XElement actionNode = XmlHelper.GetElementsByCondition(root, "CommonAction", "Id", actionId).FirstOrDefault<XElement>();
                    Action a = new Action(actionNode,root);
                    this.dictElements.Add("Action" + counter.ToString(), a);
                }
                else if (nodeName.IndexOf("Step") > -1)
                {
                    this.dictElements.Add("Step" + counter.ToString(), new Step(e, root));
                }
                counter += 1;
            }



        }
    }
}
