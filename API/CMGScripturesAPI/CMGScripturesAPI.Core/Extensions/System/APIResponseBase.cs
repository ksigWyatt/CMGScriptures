using System.Runtime.Serialization;

namespace CMGScripturesAPI.Core
{
    /// <summary>
    /// Base class that stores system responses, including errors and response objects
    /// </summary>
    public class APIResponseBase
    {
        /// <summary>
        /// Flag for if the returning method encountered some error
        /// </summary>
        [DataMember]
        public bool HasErrors { get; set; }

        /// <summary>
        /// Descriptive error message
        /// </summary>
        [DataMember]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Success message
        /// </summary>
        [DataMember]
        public string SuccessMessage { get; set; }
    }
}
