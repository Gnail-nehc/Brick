using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace Tangram.UILayer.WebControls
{
    public class WebEdit : IControl
    {
        private HtmlEdit webedit;

        public WebEdit(List<UIProperty> controlProps)
        {
            BrowserWindow window = this.GetBrowser(controlProps);
            webedit = new HtmlEdit(window);
            controlProps.ForEach(property => webedit.SearchProperties[property.PropertyName] = property.PropertyValue);
        }

        public void Set(string text)
        {
            webedit.Text = text;
        }

        public string GetText()
        {
            return webedit.Text;
        }

        public void InvokeMethod(string method, string param)
        {
            Dictionary<string, Action> dictMethod = new Dictionary<string, Action>() { 
            {"Set",()=>Set(param)}
            };

            dictMethod[method]();
        }

        public string InvokeOutputMethod(string method, string param)
        {
            Dictionary<string, Func<string>> dictMethod = new Dictionary<string, Func<string>>() { 
            {"GetText",()=>GetText()}
            };
            return dictMethod[method]();
        }
    }
}
