using CMGScripturesAPI.Repos.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMGScripturesAPI.Repos.System {
    public static class CmgJsonConverter {
        public static List<CmgImageDto> ConvertImageResponseToDto(string responseJson) {
            var result = JsonConvert.DeserializeObject<CmgResponseOuterDto>(responseJson);
            return result.images.ToList();
        }
    }
}
