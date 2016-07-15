using System;
using System.Reflection;
using log4net;
using log4net.Config;

namespace LxUtilities.Compact.Logger.Log4Net
{
    public class Logger : Contracts.Logging.ILogger
    {
        protected static readonly ILog Log;

        static Logger()
        {
            XmlConfigurator.Configure();
            Log = LogManager.GetLogger(Assembly.GetCallingAssembly().FullName);
        }

        public void LogInfo(string message)
        {
            Log.Info(message);
        }

        public void LogTrace(string message)
        {
            Log.Debug(message);
        }

        public void LogException(Exception ex)
        {
            Log.Error(ex.ToString());
        }

        public void LogException(Guid exceptionId, Exception ex)
        {
            Log.Error(exceptionId, ex);
        }
    }
}
