using System;

namespace Shearlegs.Core.Plugins.Result
{
    public class PluginErrorResult : PluginResult
    {
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionString { get; set; }

        public PluginErrorResult(string message, Exception exception)
        {
            Message = message;
            if (exception != null)
            {
                ExceptionMessage = exception.Message;
                ExceptionString = exception.ToString();
            }
        }

        public PluginErrorResult() { }
    }
}
