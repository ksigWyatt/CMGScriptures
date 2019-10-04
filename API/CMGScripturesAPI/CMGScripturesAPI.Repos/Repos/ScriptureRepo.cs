using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using CMGScripturesAPI.Core;
using System.Linq;
using System.Collections.Generic;


namespace CMGScripturesAPI.Repos
{
    /// <summary>
    /// Repository for scriptures
    /// </summary>
    public class ScriptureRepo: RepositoryBase, IScriptureRepo
    {
        private readonly IMongoCollection<ScriptureCache> _collection;

        public ScriptureRepo(IConfiguration Configuration)
            : base(Configuration, CollectionConstants.ScriptureCache)
        {
            _collection = DB.GetCollection<ScriptureCache>(CollectionConstants.ScriptureCache);

            GenerateIndexes();
        }

        /// <summary>
        /// Generates the collection's indicies
        /// </summary>
        private void GenerateIndexes()
        {
            IEnumerable<CreateIndexModel<ScriptureCache>> indexes = new List<CreateIndexModel<ScriptureCache>>
            {
                new CreateIndexModel<ScriptureCache>(new IndexKeysDefinitionBuilder<ScriptureCache>().Ascending(j => j.Reference),
                    new CreateIndexOptions
                    {
                        Name = IndexKeys.ScriptureCacheKey,
                        Background = false,
                        Unique = true
                    }
                )
            };

            GenerateIndexes(_collection, indexes);
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
            return new APIResponse<string>(true, "Not ready yet.");
        }
    }
}
