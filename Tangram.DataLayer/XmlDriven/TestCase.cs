using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml.Linq;

namespace Tangram.DataLayer.XmlDriven
{
    public class TestCase:AssetBase
    {
        private List<Scenario> scenarios=new List<Scenario>();

        public List<Scenario> Scenarios
        {
            get { return scenarios; }
        }

        public TestCase(XElement element,XElement root)
            : base(element, "TestCase")
        {
            //Get all scenarios which enable are true
            List<XElement> eleScenarios = XmlHelper.GetElementsByCondition(element, "Scenario", "Enable", "true");
            eleScenarios.ForEach(scenario => this.scenarios.Add(new Scenario(scenario,root)));
        }
    }
}
