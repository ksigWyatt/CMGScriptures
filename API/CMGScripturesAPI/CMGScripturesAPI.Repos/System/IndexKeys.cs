using System;
using System.Collections.Generic;
using System.Text;

namespace CMGScripturesAPI.Repos
{
    /// <summary>
    /// Index names to be used within each colleciton
    /// </summary>
    public class IndexKeys
    {
        /*
         * PLEASE FOLLOW THE FOLLOWING PATTERN
         * 
         * CollectionName_FieldName_SortDirection
         * 
         *  - Where sort direction is either -1 (descending) OR 1 (ascending)
         *  - Each part of the index name is dilimited by an underscore character (_)
         *  - In the event that there are multiple fields being indexed as part of a compound index
         *      - You may add an _ character between the fields
         *          EX) CollectionName_FieldName_FieldName_1
         *          
         *  - When using a TTL index, please include the expiration time within the index name
         *      - Include TTL as a phrase within the name of the index (AFTER the field names BUT NOT BEFORE the sort order)
         *      - NOTE: Do not use seconds to denote the expiration time, please only use; days, minutes or hours
         *          EX) CollectionName_FieldName_TTL_12Hours_1 
         * 
         */

        #region Scriptures

        public const string ScriptureCacheKey = "ScriptureCache_Reference_1";

        #endregion

        #region CMG Graphics

        public const string CMGImageCache = "CMGImages_CreateDate_TTL_12Hours_1";

        #endregion
    }
}
