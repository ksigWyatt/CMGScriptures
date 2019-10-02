using CMGScripturesAPI.Core;
using System;
using System.Threading.Tasks;

namespace CMGScripturesAPI.Services
{
    public class ScripturesService: IScripturesService
    {
        public ScripturesService()
        {

        }

        /// <summary>
        /// Search for a bible passage
        /// </summary>
        /// <param name="passageReference"></param>
        /// <param name="version"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public async Task<APIResponse<string>> GetPassageForSearch(string passageReference, string version, string language)
        {
            throw new NotImplementedException();
        }
    }
}