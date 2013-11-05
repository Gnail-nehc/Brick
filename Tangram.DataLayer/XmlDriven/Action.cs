using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Tangram.DataLayer.XmlDriven
{
    public class Action : AssetBase
    {
        private List<Step> steps = new List<Step>();

        public List<Step> Steps
        {
            get { return steps; }
        }


        public Action(XElement element,XElement root)
            : base(element, "Action")
        {
            element.Elements("Step").ToList<XElement>().ForEach(step=>this.steps.Add(new Step(step,root)));
        }
    }
}
