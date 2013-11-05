using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace Tangram.UILayer.WebControls
{
    public class WebInputButton : IControl
    {
        private HtmlInputButton inputbutton;

        public WebInputButton(List<UIProperty> controlProps)
        {
            BrowserWindow window = this.GetBrowser(controlProps);
            inputbutton = new HtmlInputButton(window);
            controlProps.ForEach(property => inputbutton.SearchProperties[property.PropertyName] = property.PropertyValue);
        }

        public void Click()
        {
            Mouse.Click(inputbutton);
        }

        public string GetText()
        {
            return inputbutton.DisplayText;
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
