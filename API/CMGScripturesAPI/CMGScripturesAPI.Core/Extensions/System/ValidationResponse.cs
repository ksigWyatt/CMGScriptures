using System;
using System.Collections.Generic;
using System.Text;

namespace CMGScripturesAPI.Core
{
    /// <summary>
    /// Generic validation response used to validate request objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidationResponse: APIResponseBase
    {
        /// <summary>
        /// Failure C'tor
        /// </summary>
        /// <param name="DidError"></param>
        /// <param name="ErrorMsg"></param>
        public ValidationResponse(bool DidError, string ErrorMsg)
        {
            HasErrors = DidError;
            ErrorMessage = ErrorMsg;
        }

        /// <summary>
        /// Success C'tor
        /// </summary>
        /// <param name="SuccessMsg"></param>
        public ValidationResponse(string SuccessMsg)
        {
            SuccessMessage = SuccessMsg;
        }
    }
    
}
