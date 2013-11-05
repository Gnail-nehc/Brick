using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Tangram.UILayer.WebControls;

namespace Tangram.UILayer
{
    public static class ExtendedTool
    {
        public static BrowserWindow GetBrowser(this IControl control,List<UIProperty> properties)
        {
            bool hasDifferentBrowser = properties.Exists(uiproperty => uiproperty.PropertyName.Equals("bName"));
            DefaultBrowser b = new DefaultBrowser();
            if (hasDifferentBrowser)
            {
                string propValue = properties.Find(uiproperty => uiproperty.PropertyName.Equals("bName")).PropertyValue;
                b.SearchProperties[HtmlControl.PropertyNames.Name] = propValue;
            }
            return b;
        }

    }
}
