using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAdapter.Profile.ErrorTracing
{
    public class FileErrorTracing
    {
        #region -- Properties --
        static TraceSwitch WebSvcTraceSwitch { get; set; }
        static TextWriterTraceListener TraceFileListner { get; set; }
        public static string TracFileName { get; set; }

        static bool IsTracingEnabled;

        #endregion

        #region -- Constructor --
        /// <summary>
        /// Constructor- Initialise staic variables
        /// </summary>
        static FileErrorTracing()
        {
            WebSvcTraceSwitch = new TraceSwitch(Constants.webSvcTraceSwitch, Constants.WebServiceTrace);
            TracFileName = ConfigurationManager.AppSettings[Constants.traceFileLocation];
            TraceFileListner = new TextWriterTraceListener(TracFileName);
            string traceSetUp = System.Configuration.ConfigurationManager.AppSettings[Constants.IsTracingEnabled];
            bool.TryParse(traceSetUp, out IsTracingEnabled);
            Trace.Listeners.Add(TraceFileListner);
        }
        #endregion

        #region -- Public Methods --
        /// <summary>
        /// Traces the helper.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public static void Log(string info)
        {
            if (IsTracingEnabled)
            {
                Trace.AutoFlush = true;
                Trace.WriteLineIf(true, info);
                TraceFileListner.Dispose();
            }
        }
        #endregion
    }
}
