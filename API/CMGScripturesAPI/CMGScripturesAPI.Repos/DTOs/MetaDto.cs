using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMGScripturesAPI.Repos.DTOs {
    public class MetaDto {
        [JsonProperty]
        public int RequestTotal { get; set; }
        [JsonProperty]
        public int RequestStart { get; set; }
        [JsonProperty]
        public int RequestEnd { get; set; }
        [JsonProperty]
        public int Total { get; set; }
    }
}
