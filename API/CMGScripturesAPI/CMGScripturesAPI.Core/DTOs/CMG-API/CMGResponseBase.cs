using Newtonsoft.Json;
using System.Collections.Generic;

namespace CMGScripturesAPI.Core
{
    public class CMGResponseBase
    {
        [JsonProperty("media")]
        public IEnumerable<CMGImage> images;

        public CMGResponseMeta meta;
    }
}
