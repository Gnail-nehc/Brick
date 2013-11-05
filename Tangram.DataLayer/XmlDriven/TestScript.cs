using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Tangram.UILayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tangram.UILayer.WebControls;


namespace Tangram.DataLayer.XmlDriven
{
    public class TestScript
    {
        private string testcaseId;
        private string xmlpath;
        private Log log;
        private GlobalData global;
        private Dictionary<string, string> parameters = new Dictionary<string, string>();

        public TestScript(string strId, string filepath,TestContext text)
        {
            testcaseId = strId;
            xmlpath = filepath;
            log = new Log(text);
        }

        private void Initialization(string uri)
        {
            DefaultBrowser app = new DefaultBrowser();
            app.CloseBrowser();
            //BrowserWindow.ClearCache();
            //BrowserWindow.ClearCookies();
            app.LaunchUrl(uri);
            app.Maximized = true;
        }

        public void Execute()
        {
            XElement doc = XmlHelper.LoadXml(xmlpath);
            this.global=new GlobalData(doc);
            this.Initialization(global.GetData("Uri"));

            XElement eleTc = XmlHelper.GetElementsByCondition(doc, "TestCase", "Id", testcaseId)[0];
            TestCase tc = new TestCase(eleTc,doc);
            log.Report(LogType.Info, "Start to run Test Case # {0}.\r\nDescription: {1}", tc.Id, tc.Description);
            foreach (Scenario s in tc.Scenarios)
            {
                log.Report(LogType.BeginScenario, "#{0} .Description: {1}", s.Id, s.Description);
                foreach (var operation in s.Operations)
                {
                    if (operation.Key.IndexOf("Action") == 0)
                    {
                        Action a = operation.Value as Action;
                        log.Report(LogType.BeginAction, "#{0} .Description: {1}", a.Id, a.Description);
                        a.Steps.ForEach(stepInAction => InvokeStep(stepInAction));
                    }
                    else if (operation.Key.IndexOf("Step") == 0)
                    {
                        Step step = operation.Value as Step;
                        InvokeStep(step);
                    }
                }
            }
        }

        private void InvokeStep(Step s)
        {
            try
            {
                string type = s.ControlType;
                List<UIProperty> props = s.UIProperties;
                string method = s.Method;
                string input = s.Input;
                string outparameter = "";
                if (s.HasGlobalDataAsInput)
                {
                    input = input.Substring(1, input.Length - 2);
                    input = this.global.GetData(input);
                }
                else if (s.HasOutputVariableAsInput)
                {
                    string parameterName = input.Substring(1, input.Length - 2);
                    bool hasParameterOutput = parameters.ContainsKey(parameterName);
                    if (hasParameterOutput)
                    {
                        input = parameters[parameterName];
                    }
                    else
                    {
                        log.Report(LogType.Error, "The input variable hasn't be assigned or output by previous steps.");
                        return;
                    }
                }
                log.Report(LogType.BeginStep, " {0} ({1}) -> {2} .\r\n", s.ControlType, s.ControlName, s.Method);
                if (!s.HasOutput)
                {
                    OperationGenerator.Generate(type, props, method, input);
                }
                else
                {
                    OperationGenerator.Generate(type, props, method, input, out outparameter);
                    parameters.Add(s.Output, outparameter);
                }
            }
            catch (Exception ex)
            {
                log.Report(LogType.Error, ex.Message);
            }
            finally
            {
                log.Report(LogType.EndStep,"\nStep: {0} ({1}) -> {2} .\n", s.ControlType, s.ControlName, s.Method);
            }

        }
    }
}
