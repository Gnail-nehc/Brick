using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace Tangram.UILayer.WebControls
{
    public class WebCheckBox:IControl
    {
        private HtmlCheckBox checkbox;

        public WebCheckBox(List<UIProperty> controlProps)
        {
            BrowserWindow window = this.GetBrowser(controlProps);
            checkbox = new HtmlCheckBox(window);
            controlProps.ForEach(property => checkbox.SearchProperties[property.PropertyName] = property.PropertyValue);
        }

        public void Change(bool doesCheck)
        {
            checkbox.Checked = doesCheck;
        }

        public bool IsChecked()
        {
            return checkbox.Checked;
        }

        public void InvokeMethod(string method, string param)
        {
            Dictionary<string, Action> dictMethod = new Dictionary<string, Action>() { 
            {"Change",()=>Change(param=="0"?false:true)}
            };

            dictMethod[method]();
        }

        public string InvokeOutputMethod(string method, string param)
        {
            Dictionary<string, Func<string>> dictMethod = new Dictionary<string, Func<string>>() { 
            {"IsChecked",()=>IsChecked()?"1":"0"}
            };
            return dictMethod[method]();
        }
    }
}
