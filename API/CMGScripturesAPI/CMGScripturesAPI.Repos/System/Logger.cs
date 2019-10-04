using CMGScripturesAPI.Core;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMGScripturesAPI.Repos
{
    /// <summary>
    /// Portable logger methods
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Log an error
        /// </summary>
        /// <param name="errorMessage"></param>
        public void LogError(string errorMessage)
        {
            Log.Error(string.Format(APIMessages.ErrorMessage, errorMessage));
        }

        /// <summary>
        /// Log a debug message
        /// </summary>
        /// <param name="debugMessage"></param>
        public void LogDebug(string debugMessage)
        {
            Log.Debug(string.Format(APIMessages.DebugMessage, debugMessage));
        }

        /// <summary>
        /// Log an info message
        /// </summary>
        /// <param name="infoMessage"></param>
        public void LogInfo(string infoMessage)
        {
            Log.Information(string.Format(APIMessages.InfoMessage, infoMessage));
        }
    }
}
