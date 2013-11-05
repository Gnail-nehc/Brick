using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tangram.UILayer.WebControls;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace Tangram.UILayer
{
    public class OperationGenerator
    {
        private static IControl GetControl(string uitype, List<UIProperty> properties)
        {
            switch (uitype)
            {
                case "WebCheckBox":
                    return new WebCheckBox(properties);
                case "WebComboBox":
                    return new WebComboBox(properties);
                case "WebEdit":
                    return new WebEdit(properties);
                case "WebInputButton":
                    return new WebInputButton(properties);
                case "WebLink":
                    return new WebLink(properties);
                case "WebTable":
                    return new WebTable(properties);
                default:
                    Console.WriteLine("Not found control.");return null;
            }
        }

        public static void Generate(string uitype, List<UIProperty> properties, string method, string param)
        {
            IControl control = GetControl(uitype, properties);
            control.InvokeMethod(method, param);
        }

        public static void Generate(string uitype, List<UIProperty> properties, string method, string param, out string output)
        {
            IControl control = GetControl(uitype, properties);
            output = control.InvokeOutputMethod(method, param);
        }
    
    }
}
