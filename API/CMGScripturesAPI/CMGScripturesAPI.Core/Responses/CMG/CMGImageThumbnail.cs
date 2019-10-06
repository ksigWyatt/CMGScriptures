using System;
using System.Collections.Generic;
using System.Text;

namespace CMGScripturesAPI.Core
{
    /// <summary>
    /// Thumbnail to display on screen in which a user can choose
    /// </summary>
    public class CMGImageThumbnail
    {
        /// <summary>
        /// Unique Id for this image
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Timestamp for when this image was uploaded
        /// </summary>
        public long Added { get; set; }

        /// <summary>
        /// URL string for the image thumbnail
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// We will only use this in the event that a user clicks on this thumbnail
        /// </summary>
        public string ThumbnailXL { get; set; }

        // These next 2 are constants on the CMG API, as far as I can tell which makes
        // UI developement a bit easier

        /// <summary>
        /// Width of this thumbnail
        /// </summary>
        public const int Width = 500;

        /// <summary>
        /// Height of this thumbnail
        /// </summary>
        public const int Height = 281;
    }
}