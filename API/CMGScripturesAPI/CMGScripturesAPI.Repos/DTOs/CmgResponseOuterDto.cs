using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMGScripturesAPI.Repos.DTOs {
    public class CmgResponseOuterDto {
        [JsonProperty("media")]
        public CmgImageDto[] images;
        public MetaDto meta;
    }
}
