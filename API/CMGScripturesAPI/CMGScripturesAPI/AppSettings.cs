using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMGScripturesAPI
{
    public class AppSettings
    {
        /// <summary>
        /// Connection string to your MongoDB instance
        /// </summary>
        public string MongoConnectionString { get; set; }

        /// <summary>
        /// Public CMG API url - used for making requests against CMG
        /// </summary>
        public string CMGApiUrl { get; set; }
    }
}
