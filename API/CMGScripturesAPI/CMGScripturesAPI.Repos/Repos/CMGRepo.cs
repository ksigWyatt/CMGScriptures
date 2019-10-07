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
            : base(Configuration, CollectionConstants.CMGCache)
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

            var uri = string.Format("{0}media?active=true&format=still&start={1}", CMG_API_URL, startNumer);
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

            // we DO NOT want to await this, just fire and forget this
            _ = SetCMGCache(cmgResponse.images);

            return new APIResponse<CMGThumbnailResponse>(response, "Success!");
        }

        /// <summary>
        /// Sets or updates the CMG cache for any number of objects
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task SetCMGCache(IEnumerable<CMGImage> request)
        {
            if (request == null || !request.Any())
            {
                // we  don't want to spin our wheels if there's nothing to do
                return;
            }

            // create an $in filter so that we can search for the coorisponding documents and update them if needed
            // this method IS GOING TO BE HIT FREQUENTLY, so we can't just always update the documents, we need to make sure that we
            // only insert them if they don't exist

            var filter = Builders<CMGCache>.Filter.In(c => c.MediaId, request.Select(i => i.Id));
            var cursor = await _collection.FindAsync(filter);

            // in the event that there are no documents that we found, we should just insert all of these
            if (cursor.ToEnumerable().Count() == 0)
            {
                var updateList = new List<WriteModel<CMGCache>>();

                foreach (var mediaItem in request)
                {
                    var item = new CMGCache
                    {
                        Added = mediaItem.Added,
                        CreateDate = DateTime.UtcNow,
                        Creator = mediaItem.Creator,
                        Filename = mediaItem.Filename,
                        Height = mediaItem.Height,
                        MediaId = mediaItem.Id,
                        Thumbnail = mediaItem.Thumbnail,
                        ThumbnailLg = mediaItem.ThumbnailLg,
                        ThumbnailXLg = mediaItem.ThumbnailXLg,
                        Width = mediaItem.Width
                    };

                    var updateModel = new InsertOneModel<CMGCache>(item);

                    updateList.Add(updateModel);
                }

                // we will assign this Task to a discard, because we don't really care about the result of this
                _ = _collection.BulkWriteAsync(updateList);

                return;
            }

            // if there are items in the database that have expired, we need to make sure that we re-insert them
            if (cursor.ToEnumerable().Count() != request.Count())
            {
                var updateList = new List<WriteModel<CMGCache>>();

                foreach (var mediaItem in request)
                {
                    var updateFilter = Builders<CMGCache>.Filter.Eq(c => c.MediaId, mediaItem.Id);
                    var updateDefinition = Builders<CMGCache>.Update
                        .Set(c => c.Added, mediaItem.Added)
                        .Set(c => c.CreateDate, DateTime.UtcNow)
                        .Set(c => c.Creator, mediaItem.Creator)
                        .Set(c => c.Filename, mediaItem.Filename)
                        .Set(c => c.Height, mediaItem.Height)
                        .Set(c => c.MediaId, mediaItem.Id)
                        .Set(c => c.Thumbnail, mediaItem.Thumbnail)
                        .Set(c => c.ThumbnailLg, mediaItem.ThumbnailLg)
                        .Set(c => c.ThumbnailXLg, mediaItem.ThumbnailXLg)
                        .Set(c => c.Width, mediaItem.Width);

                    var updateModel = new UpdateOneModel<CMGCache>(updateFilter, updateDefinition);

                    updateList.Add(updateModel);
                }

                // we will assign this Task to a discard, because we don't really care about the result of this
                _ = _collection.BulkWriteAsync(updateList);

                return;
            }
        }
    }
}
