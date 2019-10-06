using System;
using System.Collections.Generic;
using System.Text;

namespace CMGScripturesAPI.Core
{
    /// <summary>
    /// CMG thumbnail object from CMG API
    /// </summary>
    [Serializable]
    public class CMGCache: ObjectBase
    {
        public CMGCache()
        {
            CreateDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Unique Id of this media item
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// Timestamp for when this item was uploaded
        /// </summary>
        public long Added { get; set; }

        /// <summary>
        /// Width of the full size in pixels
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height of the full size in pixels
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The organization that created this image
        /// </summary>
        public CMGImageCreator Creator { get; set; }

        /// <summary>
        /// Encoded URL to the thumbnail
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// Encoded URL to a better quality nail
        /// </summary>
        public string ThumbnailLg { get; set; }

        /// <summary>
        /// Encoded URL to the full size thumbnail
        /// </summary>
        public string ThumbnailXLg { get; set; }

        /// <summary>
        /// Name of the media item
        /// </summary>
        public string Filename { get; set; }
    }
}