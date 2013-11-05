using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace Tangram.UILayer.WebControls
{
    public class WebComboBox : IControl
    {
        private HtmlComboBox webcombo;

        public WebComboBox(List<UIProperty> controlProps)
        {
            BrowserWindow window = this.GetBrowser(controlProps);
            webcombo = new HtmlComboBox(window);
            controlProps.ForEach(property => webcombo.SearchProperties[property.PropertyName] = property.PropertyValue);
        }

        public void Select(string item)
        {
            webcombo.SelectedItem = item;
        }

        public string GetText()
        {
            return webcombo.InnerText;
        }

        public void InvokeMethod(string method, string param)
        {
            Dictionary<string, Action> dictMethod = new Dictionary<string, Action>() { 
            {"Select",()=>Select(param)}
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
