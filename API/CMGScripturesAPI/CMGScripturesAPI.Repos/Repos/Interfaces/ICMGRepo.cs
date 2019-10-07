using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CMGScripturesAPI.Core;

namespace CMGScripturesAPI.Repos
{
    public interface ICMGRepo
    {
        /// <summary>
        /// Get thumbnails for user selection
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        Task<APIResponse<CMGThumbnailResponse>> GetPagedThumbnails(PagingRequest paging);
    }
}
