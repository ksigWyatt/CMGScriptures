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
    public class CMGRepo: RepositoryBase, ICMGRepo
    {
        private readonly IMongoCollection<CMGCache> _collection;

        public CMGRepo(IConfiguration Configuration)
            : base(Configuration, null)
        {
            _collection = DB.GetCollection<CMGCache>(CollectionConstants.CMGCache);

            GenerateIndexes();
        }

        /// <summary>
        /// Generates the collection's indicies
        /// </summary>
        private void GenerateIndexes()
        {
            IEnumerable<CreateIndexModel<CMGCache>> indexes = new List<CreateIndexModel<CMGCache>>
            {
                new CreateIndexModel<CMGCache>(new IndexKeysDefinitionBuilder<CMGCache>().Ascending(j => j.MediaId),
                    new CreateIndexOptions
                    {
                        Name = IndexKeys.CMGMediaId,
                        Background = false,
                        Unique = true
                    }
                ),
                new CreateIndexModel<CMGCache>(new IndexKeysDefinitionBuilder<CMGCache>().Descending(j => j.CreateDate),
                    new CreateIndexOptions
                    {
                        Name = IndexKeys.CMGImageCache,
                        Background = false
                    }
                )
            };

            GenerateIndexes(_collection, indexes);
        }

        /// <summary>
        /// Get thumbnails for user selection
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        public async Task<APIResponse<CMGThumbnailResponse>> GetPagedThumbnails(PagingRequest paging)
        {
            // we HAVE to overwrite the page count to be 36 at a time
            // This is a hard limit set by the CMG API itself
            paging.PageCount = 36;
            var startNumer =  paging.PageCount * (paging.PageNumber - 1); 

            var uri = string.Format("/media?active=true&format=still&start={0}", startNumer);
            var result = await GetAsync(uri);

            if (!result.IsSuccessStatusCode)
            {
                return new APIResponse<CMGThumbnailResponse>(true, "An error occurred communicating with the CMG API.");
            }

            var jsonString = await result.Content.ReadAsStringAsync();
            var cmgResponse = ConvertJSONToObject<CMGResponseBase>(jsonString);

            var resultList = new List<CMGImageThumbnail>();

            var response = new CMGThumbnailResponse
            {
                Paging = new PageInfo
                {
                    PageNumber = paging.PageNumber,
                    TotalPageCount = GetTotalPages(cmgResponse.meta.Total, 36),
                    TotalRecordCount = cmgResponse.meta.Total
                }
            };

            foreach (var media in cmgResponse.images)
            {
                resultList.Add(new CMGImageThumbnail
                {
                    Added = media.Added,
                    Id = media.Id,
                    Thumbnail = media.Thumbnail,
                    ThumbnailXL = media.ThumbnailXLg
                });
            }

            response.Thumbnails = resultList;

            return new APIResponse<CMGThumbnailResponse>(response, "Success!");
        }
    }
}
