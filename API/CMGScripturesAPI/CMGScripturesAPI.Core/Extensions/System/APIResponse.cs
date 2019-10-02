using Serilog;

namespace CMGScripturesAPI.Core
{
    /// <summary>
    /// Generic system response messages that can be used for any reason
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class APIResponse<T> : APIResponseBase
    {
        /// <summary>
        /// The data from the response, which is of generic type
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// Failure C'tor
        /// </summary>
        /// <param name="DidError"></param>
        /// <param name="ErrorMsg"></param>
        public APIResponse(bool DidError, string ErrorMsg)
        {
            HasErrors = DidError;
            ErrorMessage = ErrorMsg;

            Log.Error(string.Format(APIMessages.ErrorMessage, ErrorMsg));
        }

        /// <summary>
        /// Success C'tor
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="SuccessMsg"></param>
        public APIResponse(T Value, string SuccessMsg)
        {
            Result = Value;
            SuccessMessage = SuccessMsg;
        }

        /// <summary>
        /// Success C'tor to use without a response object
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="SuccessMsg"></param>
        public APIResponse(string SuccessMsg)
        {
            SuccessMessage = SuccessMsg;
        }
    }
    
}
