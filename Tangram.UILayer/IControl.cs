using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tangram.UILayer
{
    public interface IControl
    {
        void InvokeMethod(string method,string param);
        string InvokeOutputMethod(string method, string param);
    }
}
