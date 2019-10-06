using CMGScripturesAPI.Core;
using CMGScripturesAPI.Repos;
using System;
using System.Threading.Tasks;

namespace CMGScripturesAPI.Services
{
    public class CMGService: ICMGService
    {
        private readonly ICMGRepo _cmgRepo;

        public CMGService(ICMGRepo cmgRepo)
        {
            _cmgRepo = cmgRepo;
        }

        /// <summary>
        /// Get thumbnails for user selection
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        public async Task<APIResponse<CMGThumbnailResponse>> GetPagedThumbnails(PagingRequest paging)
        {
            // validate the request object 
            var validationResponse = paging.ValidatePaging();
            if (validationResponse.HasErrors)
            {
                return new APIResponse<CMGThumbnailResponse>(true, validationResponse.ErrorMessage);
            }

            var cmgResponse = await _cmgRepo.GetPagedThumbnails(paging);
            if (cmgResponse.HasErrors)
            {
                return new APIResponse<CMGThumbnailResponse>(true, cmgResponse.ErrorMessage);
            }


            return new APIResponse<CMGThumbnailResponse>(cmgResponse.Result, "Success!");
        }
    }
}
