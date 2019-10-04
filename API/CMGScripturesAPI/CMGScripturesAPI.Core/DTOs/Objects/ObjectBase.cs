using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace CMGScripturesAPI.Core
{
    /// <summary>
    /// MongoDB object base used to inherit Id and CreateDate
    /// </summary>
    public class ObjectBase
    {
        /// <summary>
        /// Object Id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Timestamp for when this object was created (UTC)
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
