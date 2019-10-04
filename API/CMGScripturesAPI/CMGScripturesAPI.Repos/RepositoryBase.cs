using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using CMGScripturesAPI.Core;
using MongoDB.Bson;
using System.Collections.Generic;
using MongoDB.Bson.Serialization;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Linq;

namespace CMGScripturesAPI.Repos
{
    public class RepositoryBase: Logger
    {
        #region Read-Only Configs

        /// <summary>
        /// Readonly MongDB Connection String
        /// </summary>
        public readonly string MongoConnectionString;

        /// <summary>
        /// Readonly API Url for CMG
        /// </summary>
        public readonly string CMG_API_URL;

        /// <summary>
        /// Readonly current Mongo Database
        /// </summary>
        public readonly IMongoDatabase DB;

        /// <summary>
        /// Public access to the Mongo Client
        /// </summary>
        public readonly MongoClient Client;

        /// <summary>
        /// Readonly collection options for creating new collections in the Database
        /// </summary>
        private readonly CreateCollectionOptions CreateCollectionOptions = new CreateCollectionOptions
        {
            Collation = new Collation(locale: "en_US", caseLevel: false, strength: CollationStrength.Secondary)
        };

        /// <summary>
        /// Readonly aggregation pipeline options
        /// </summary>
        public readonly AggregateOptions GlobalAggregateOptions = new AggregateOptions
        {
            AllowDiskUse = true
        };

        #endregion

        #region Private Vars

        private readonly string _userAgent = "HttpClient";

        private const string DB_NAME = "CMG_Scriptures";

        #endregion

        protected RepositoryBase(IConfiguration Configuration, string collectionName)
        {
            // set our keys and connections here in our Base Class
            MongoConnectionString = Configuration["MongoConnectionString"];
            CMG_API_URL = Configuration["CMGApiUrl"];

            // in the event our configs are null, throw a Null Exception
            ValidateConfigs(Configuration);

            // assuming the configs are valid, create a MongoClient we can use for everything, we only need one.
            // Sets the Client object for a given connection string
            Client = new MongoClient(MongoConnectionString);

            // assign our database to the global Abide Database, this will also create it if it does not exist
            DB = Client.GetDatabase(DB_NAME);

            // safely create new collections
            CreateNewCollection(collectionName);
        }

        /// <summary>
        /// Creates a new collection with the specified name and settings. Protects from exceptions.
        /// </summary>
        /// <param name="collectionName"></param>
        private void CreateNewCollection(string collectionName)
        {
            try
            {
                DB.CreateCollection(collectionName, CreateCollectionOptions);

                LogDebug($"Collection {DB_NAME}.{collectionName} successfully created.");
            }
            catch (MongoCommandException e)
            {
                if (e.Message == $"Command create failed: a collection '{DB_NAME}.{collectionName}' already exists.")
                {
                    // this is expected, because this collection already exists, so just ignore this exception
                    return;
                }
            }
        }

        /// <summary>
        /// The service cannot start if the settings are not present
        /// </summary>
        private void ValidateConfigs(IConfiguration Configuration)
        {
            if (string.IsNullOrEmpty(MongoConnectionString))
            {
                throw new ArgumentNullException("IConfiguration.MongoConnectionString",
                    string.Format(APIMessages.ConnectionMissingFromAppSettings, "MongoConnectionString"));
            }

            if (string.IsNullOrEmpty(CMG_API_URL))
            {
                throw new ArgumentNullException("IConfiguration.CMGApiUrl",
                    string.Format(APIMessages.ConnectionMissingFromAppSettings, "CMGApiUrl"));
            }
        }

        /// <summary>
        /// Generates a collection's indicies
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="indicies"></param>
        protected void GenerateIndexes<T>(IMongoCollection<T> collection, IEnumerable<CreateIndexModel<T>> indicies)
        {
            collection.Indexes.CreateMany(indicies);
        }

        /// <summary>
        /// Returns the total number of pages for a collection of items in a collection, given some arbitraty page count
        /// </summary>
        /// <param name="items"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        protected static int GetTotalPages(int items, int pageCount)
        {
            return (items + pageCount - 1) / pageCount;
        }

        /// <summary>
        /// Page results for find cursor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cursor"></param>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        protected static Task<List<T>> PageResults<T>(IFindFluent<T, T> cursor, PagingRequest pagingInfo)
        {
            return cursor.Skip(pagingInfo.PageCount * (pagingInfo.PageNumber - 1)).Limit(pagingInfo.PageCount).ToListAsync();
        }

        /// <summary>
        /// Page results from a pre-sorted find result set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cursor"></param>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        protected static IEnumerable<T> PageResults<T>(IOrderedEnumerable<T> elements, PagingRequest pagingInfo)
        {
            return elements.Skip(pagingInfo.PageCount * (pagingInfo.PageNumber - 1)).Take(pagingInfo.PageCount);
        }
    }
}
