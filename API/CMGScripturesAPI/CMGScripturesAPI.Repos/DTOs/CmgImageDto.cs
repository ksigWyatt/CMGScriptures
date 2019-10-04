using System;
using System.Collections.Generic;
using System.Text;

namespace CMGScripturesAPI.Repos.DTOs {
    public class CmgImageDto {
        public string Id { get; set; }
        //public string Permalink { get; set; }
        //public string Name { get; set; }
        //public string Type { get; set; }
        public DateTime Added { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public CreatorDto Creator { get; set; }
        public string Thumbnail { get; set; }
        public string ThumbnailLg { get; set; }
        public string ThumbnailXLg { get; set; }
        //public int Downloads { get; set; }
        //public bool IsFree { get; set; }
        public string Filename { get; set; }



    }
}
