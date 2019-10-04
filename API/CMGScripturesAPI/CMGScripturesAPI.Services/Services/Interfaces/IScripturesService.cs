using CMGScripturesAPI.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMGScripturesAPI.Services
{
    public interface IScripturesService
    {
        /// <summary>
        /// Search for a bible passage
        /// </summary>
        /// <param name="passageReference"></param>
        /// <param name="version"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        Task<APIResponse<string>> GetPassageForSearch(string passageReference, string version, string language);
    }
}
