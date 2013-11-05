using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace Tangram.UILayer.WebControls
{
    public class WebTable : IControl
    {
        private HtmlTable table;

        public WebTable(List<UIProperty> controlProps)
        {
            BrowserWindow window = this.GetBrowser(controlProps);
            table = new HtmlTable(window);
            controlProps.ForEach(property => table.SearchProperties[property.PropertyName] = property.PropertyValue);
        }

        private HtmlCell GetCellByIndex(int rowIndex, int columnIndex)
        {
            HtmlCell cell = new HtmlCell(table);
            cell.SearchProperties[HtmlCell.PropertyNames.RowIndex] = rowIndex.ToString();
            cell.SearchProperties[HtmlCell.PropertyNames.ColumnIndex] = columnIndex.ToString();
            cell.Find();
            return cell;
        }

        public void ClickCell(int cellRowIndex, int cellColumnIndex, int indexInCell)
        { 
            HtmlCell cell=this.GetCellByIndex(cellRowIndex,cellColumnIndex);
            UITestControl control=cell.GetChildren()[indexInCell];
            Mouse.Click(control);
        }

        public string GetCellText(int cellRowIndex, int cellColumnIndex)
        {
            HtmlCell cell = this.GetCellByIndex(cellRowIndex, cellColumnIndex);
            return cell.InnerText;
        }

        public void InvokeMethod(string method, string param)
        {
            Dictionary<string, Action> dictMethod = new Dictionary<string, Action>() { 
            {"ClickCell",()=>ClickCell(Convert.ToInt32(param.Split('|')[0]),Convert.ToInt32(param.Split('|')[1]),Convert.ToInt32(param.Split('|')[2]))}
            };

            dictMethod[method]();
        }

        public string InvokeOutputMethod(string method, string param)
        {
            Dictionary<string, Func<string>> dictMethod = new Dictionary<string, Func<string>>() { 
            {"GetCellText",()=>GetCellText(Convert.ToInt32(param.Split('|')[0]),Convert.ToInt32(param.Split('|')[1]))}
            };
            return dictMethod[method]();
        }
    }
}
