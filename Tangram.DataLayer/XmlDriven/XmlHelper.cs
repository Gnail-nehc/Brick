using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data;

namespace Tangram.DataLayer.XmlDriven
{
    public class XmlHelper
    {
        public static XElement LoadXml(string filePath)
        {
            try
            {
                return XElement.Load(filePath);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static List<XElement> GetElementsByCondition(XElement element, string nodeName, string attributeName, string attributeValue)
        {
            IEnumerable<XElement> query = from elm in element.Descendants(nodeName)
                     where (string)elm.Attribute(attributeName) == attributeValue
                     select elm;
            return query.ToList<XElement>();
        }

    }
}
