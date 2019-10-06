using System;
using System.Collections.Generic;
using System.Text;

namespace CMGScripturesAPI.Core
{
    /// <summary>
    /// Used to render a selection of graphics options in a grid view on the UI
    /// </summary>
    public class CMGThumbnailResponse
    {
        /// <summary>
        /// Collection of thumbnails to render on screen to the user
        /// </summary>
        public IEnumerable<CMGImageThumbnail> Thumbnails { get; set; }

        /// <summary>
        /// Paging information used to select the next page
        /// </summary>
        public PageInfo Paging { get; set; }
    }
}