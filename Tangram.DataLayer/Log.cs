using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tangram.DataLayer
{
    public class Log
    {
        private TestContext text;

        public Log(TestContext _text)
        {
            text = _text;
        }

        public void Report(LogType type, string message, params object[] args)
        {
            if (null != text)
                text.WriteLine("\r\n" + type.ToString() + ": {0}", string.Format(message, args) + "\r\n");
        }
    }

    public enum LogType
    {
        Info,
        Debug,
        Error,
        Warning,
        BeginScenario,
        EndScenario,
        BeginAction,
        EndAction,
        BeginStep,
        EndStep,
    }

}
