using CMGScripturesAPI.Repos.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMGScripturesAPI.Repos.System {
    public static class CmgJsonConverter {
        public static IEnumerable<CmgImageDto> ExtractImageObjectsFromResponse(string responseJson) {
            var result = JsonConvert.DeserializeObject<CmgResponseOuterDto>(responseJson);
            return result.images;
        }

    }
}
