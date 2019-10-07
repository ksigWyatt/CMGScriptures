namespace CMGScripturesAPI.Core {

    public class CMGImage
    {
        public string Id { get; set; }

        public long Added { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public CMGImageCreator Creator { get; set; }

        public string Thumbnail { get; set; }

        public string ThumbnailLg { get; set; }

        public string ThumbnailXLg { get; set; }

        public string Filename { get; set; }
    }
}
