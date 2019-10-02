using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMGScripturesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CMGScripturesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScripturesController : ControllerBase
    {
        private readonly IScripturesService _scripturesService;

        /// <summary>
        /// Scriptures Controller C'tor
        /// </summary>
        public ScripturesController(IScripturesService scripturesService)
        {
            _scripturesService = scripturesService;
        }

        /// <summary>
        /// Search for a bible passage
        /// </summary>
        /// <returns>A pre-formatted bible passage</returns>
        /// <param name="passageReference"></param>
        /// <param name="version"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        /// <remarks>
        ///  
        /// </remarks>
        [Produces("application/json")]
        [HttpGet("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<string>> ScriptureSearch([BindRequired] string passageReference, string version, string language)
        {
            var response = await _scripturesService.GetPassageForSearch(passageReference, version, language);

            if (response.HasErrors)
            {
                return StatusCode(400, response.ErrorMessage);
            }

            return response.Result;
        }
    }
}
