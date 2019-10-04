namespace CMGScripturesAPI.Core
{
    public class APIMessages
    {
        #region Logging

        public const string ErrorMessage = "ERROR: {0}";
        public const string DebugMessage = "DEBUG: {0}";
        public const string InfoMessage = "INFO: {0}";
        public const string ExceptionMessage = "UNKNOWN EXCEPTION: {0}, {1}.";
        public const string UnknownExceptionOcurred = "An unknown error ocurred. Please refer to ticket number: {0}.";

        #endregion

        #region Validations

        public const string EmptyRequest = "Request cannot be null or empty.";
        public const string NullProperty = "Property named {0} cannot be null or empty.";
        public const string ConnectionMissingFromAppSettings = "{0} is a required configuation parameter. Please add it to the AppSettings.json and restart the application.";
        public const string PropertyMustBeLargerThan = "{0} must be a value larger than {1}.";
        public const string InvalidPagingOptions = "The multiplied value of PageCount and PageNumber must be less than 2147483647."; // This number is Int32.MaxValue

        #endregion
    }
}
