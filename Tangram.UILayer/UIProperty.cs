using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tangram.UILayer
{
    public class UIProperty
    {
        private string propertyName;

        public string PropertyName
        {
            get { return propertyName; }
        }

        private string propertyValue;

        public string PropertyValue
        {
            get { return propertyValue; }
        }

        public UIProperty(string _paraName, string _paraValue)
        {
            this.propertyName = _paraName;
            this.propertyValue = _paraValue;
        }
    }
}
