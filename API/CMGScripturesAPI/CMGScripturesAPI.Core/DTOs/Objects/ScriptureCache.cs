using System;
using System.Collections.Generic;
using System.Text;

namespace CMGScripturesAPI.Core
{
    /// <summary>
    /// Scripture passage that has been cached
    /// </summary>
    [Serializable]
    public class ScriptureCache: ObjectBase
    {
        /// <summary>
        /// C'tor
        /// </summary>
        public ScriptureCache()
        {
            CreateDate = DateTime.UtcNow;
            Reference = null;
            Text = null;
        }

        /// <summary>
        /// The scripture cache key made up of the passage location, version and language
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Pre-formatted passage text to display to the user
        /// </summary>
        public string Text { get; set; }
    }
}
