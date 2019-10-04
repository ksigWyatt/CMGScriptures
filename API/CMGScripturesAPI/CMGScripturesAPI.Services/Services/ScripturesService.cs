using CMGScripturesAPI.Core;
using CMGScripturesAPI.Repos;
using System;
using System.Threading.Tasks;

namespace CMGScripturesAPI.Services
{
    public class ScripturesService: IScripturesService
    {
        private readonly IScriptureRepo _scriptureRepo;

        public ScripturesService(IScriptureRepo scriptureRepo)
        {
            _scriptureRepo = scriptureRepo;
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
            #region Validations

            // Validate the request parameters -- none of which can be null
            // otherwise we don't know which version to look at on Bible.com
            if (string.IsNullOrEmpty(passageReference))
            {
                return new APIResponse<string>(true, string.Format(APIMessages.NullProperty, nameof(passageReference)));
            }
            if (string.IsNullOrEmpty(version))
            {
                return new APIResponse<string>(true, string.Format(APIMessages.NullProperty, nameof(version)));
            }
            if (string.IsNullOrEmpty(language))
            {
                return new APIResponse<string>(true, string.Format(APIMessages.NullProperty, nameof(language)));
            }

            #endregion

            // Send the request off
            var passageResponse = await _scriptureRepo.GetPassageForSearch(passageReference, version, language);
            if (passageResponse.HasErrors)
            {
                return new APIResponse<string>(true, passageResponse.ErrorMessage);
            }

            // return the request to the user
            var passage = passageResponse.Result;

            return new APIResponse<string>(passage, "Success!");
        }
    }
}