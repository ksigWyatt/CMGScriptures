namespace CMGScripturesAPI.Core
{
    /// <summary>
    /// Response metadata information, mainly used for paging
    /// </summary>
    public class CMGResponseMeta
    {
        public int RequestTotal { get; set; }

        public int RequestStart { get; set; }

        public int RequestEnd { get; set; }

        public int Total { get; set; }
    }
}