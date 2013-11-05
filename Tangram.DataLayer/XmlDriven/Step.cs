using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Tangram.UILayer;

namespace Tangram.DataLayer.XmlDriven
{
    public class Step
    {
        private string controlType;

        public string ControlType
        {
            get { return controlType; }
        }

        private string controlName;

        public string ControlName
        {
            get { return controlName; }
        }

        private string method;

        public string Method
        {
            get { return method; }
        }

        private string input;

        public string Input
        {
            get { return input; }
        }

        private bool hasOutput=false;

        public bool HasOutput
        {
            get { return hasOutput; }
        }

        private string output;

        public string Output
        {
          get { return output; }
        }

        public bool HasOutputVariableAsInput
        {
            get 
            {
                return (this.input != "" && this.input.Substring(0, 1) == "{" && this.input.Substring(this.input.Length - 1, 1) == "}");
            }
        }

        public bool HasGlobalDataAsInput
        {
            get
            {
                return (this.input != "" && this.input.Substring(0, 1) == "(" && this.input.Substring(this.input.Length - 1, 1) == ")");
            }
        }

        private List<UIProperty> uiproperties;

        public List<UIProperty> UIProperties
        {
            get { return uiproperties; }
        }


        public Step(XElement element,XElement root)
        {
            this.controlName = element.Attribute("ControlName").Value;
            this.controlType = element.Attribute("ControlType").Value;
            this.method = element.Attribute("Method").Value;
            this.input = element.Attribute("Input").Value;
            this.hasOutput = element.HasElements;
            this.output = hasOutput ? element.Element("Output").Value : "";
            this.uiproperties = GetUIPropertys(root, controlType, controlName);
        }

        private List<UIProperty> GetUIPropertys(XElement root,string controlType,string controlName)
        {
            List<UIProperty> properties = new List<UIProperty>();
            XElement type = XmlHelper.GetElementsByCondition(root,"Type","Name",controlType).FirstOrDefault<XElement>();
            XElement control=XmlHelper.GetElementsByCondition(type,"Control","ControlName",controlName).FirstOrDefault<XElement>();
            control.Elements("P").ToList<XElement>().ForEach(p => properties.Add(new UIProperty(p.Attribute("PropertyName").Value,p.Value)));
            return properties;
        }
    }
}
