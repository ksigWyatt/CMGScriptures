using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMGScripturesAPI.Repos.DTOs {
    public class MetaDto {
        public int RequestTotal { get; set; }
        public int RequestStart { get; set; }
        public int RequestEnd { get; set; }
        public int Total { get; set; }
    }
}
