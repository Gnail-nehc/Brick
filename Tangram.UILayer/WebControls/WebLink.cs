using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace Tangram.UILayer.WebControls
{
    public class WebLink : IControl
    {
        private HtmlHyperlink weblink;

        public WebLink(List<UIProperty> controlProps)
        {
            BrowserWindow window = this.GetBrowser(controlProps);
            weblink = new HtmlHyperlink(window);
            controlProps.ForEach(property => weblink.SearchProperties[property.PropertyName] = property.PropertyValue);
        }

        public void Click()
        {
            Mouse.Click(weblink);
        }

        public string GetText()
        {
            return weblink.InnerText;
        }

        public void InvokeMethod(string method, string param)
        {
            Dictionary<string, Action> dictMethod = new Dictionary<string, Action>() { 
            {"Click",()=>Click()}
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
