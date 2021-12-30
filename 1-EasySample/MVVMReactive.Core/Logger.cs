using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMReactive.Core
{
    public static class Logger
    {
        #region Private static property

        private static NLog.Logger logger;

        #endregion Private static property

        #region Ctor

        static Logger()
        {
            logger = LogManager.GetCurrentClassLogger();
            var isEnabled = logger.IsEnabled(LogLevel.Info);
        }

        #endregion Ctor

        #region public methods

        public static void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public static void Fatal(Exception exception, string message)
        {
            logger.Fatal(exception, message);
        }

        public static void Info(string message)
        {
            logger.Info(message);
        }

        public static void Info(Exception exception, string message)
        {
            logger.Info(exception, message);
        }

        public static void Error(string message)
        {
            logger.Error(message);
        }

        public static void Error(Exception exception, string message)
        {
            logger.Error(exception, message);
        }

        public static void Debug(string message)
        {
            logger.Debug(message);
        }

        public static void Debug(Exception exception, string message)
        {
            logger.Debug(exception, message);
        }

        #endregion public methods
    }
}