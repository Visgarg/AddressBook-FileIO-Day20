using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace AddressBook
{
    public class NLog
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Calling Debug method from logger class
        /// </summary>
        /// <param name="message"></param>
        public void LogDebug(string message)
        {
            logger.Debug(message);
        }
        /// <summary>
        /// getting error message from logger class
        /// </summary>
        /// <param name="message"></param>
        public void LogError(string message)
        {
            logger.Error(message);
        }
        /// <summary>
        /// getting log info message from nlog class.
        /// </summary>
        /// <param name="message"></param>
        public void LogInfo(string message)
        {
            logger.Info(message);
        }
        /// <summary>
        /// getting log warning class from nlog class
        /// </summary>
        /// <param name="message"></param>
        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
    }

}