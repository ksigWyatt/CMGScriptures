using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMGScripturesAPI.Core;
using CMGScripturesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CMGScripturesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMGController : ControllerBase
    {
        private readonly ICMGService _cmgService;

        /// <summary>
        /// Scriptures Controller C'tor
        /// </summary>
        public CMGController(ICMGService cmgService)
        {
            _cmgService = cmgService;
        }

        /// <summary>
        /// Get thumbnails for user selection
        /// </summary>
        /// <returns>A paged collection of CMG images</returns>
        /// <param name="passageReference"></param>
        /// <param name="version"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        [Produces("application/json")]
        [HttpGet("thumbnails")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CMGThumbnailResponse>> GetPagedThumbnails([BindRequired] PagingRequest paging)
        {
            var response = await _cmgService.GetPagedThumbnails(paging);

            if (response.HasErrors)
            {
                return StatusCode(400, response.ErrorMessage);
            }

            return response.Result;
        }
    }
}