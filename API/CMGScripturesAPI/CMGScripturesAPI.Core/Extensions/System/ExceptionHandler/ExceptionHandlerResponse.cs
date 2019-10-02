using Newtonsoft.Json;

namespace CMGScriptures.Core.System.ExceptionHandler
{
    /// <summary>
    /// Exception handler response
    /// </summary>
    public class ExceptionHandlerResponse
    {
        /// <summary>
        /// Formatted error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Convert the object to a JSON string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
