using System;
using System.Collections.Generic;
using System.Text;

namespace CMGScripturesAPI.Core
{
    /// <summary>
    /// Requested paging
    /// </summary>
    public class PagingRequest
    {
        /// <summary>
        /// C'tor
        /// </summary>
        public PagingRequest()
        {
            PageNumber = 1;
            PageCount = 10;
        }

        /// <summary>
        /// Requested page number (default = 1)
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Requested number of items per page (default = 10)
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Validate the request object
        /// </summary>
        /// <returns></returns>
        public ValidationResponse ValidatePaging()
        {
            if (this != null)
            {
                if (PageNumber <= 0)
                {
                    return new ValidationResponse(true, string.Format(APIMessages.PropertyMustBeLargerThan, nameof(PageNumber), 0));
                }

                if (PageCount <= 0)
                {
                    return new ValidationResponse(true, string.Format(APIMessages.PropertyMustBeLargerThan, nameof(PageCount), 0));
                }

                if (PageNumber * PageCount > int.MaxValue)
                {
                    return new ValidationResponse(true, APIMessages.InvalidPagingOptions);
                }
            }

            return new ValidationResponse("Success!");
        }
    }

}
