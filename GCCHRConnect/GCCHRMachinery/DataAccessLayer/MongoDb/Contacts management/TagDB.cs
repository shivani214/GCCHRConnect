using GCCHRMachinery.Entities;
using System.Collections;
using MongoDB.Driver;
using System.Collections.Generic;

namespace GCCHRMachinery.DataAccessLayer.MongoDb
{
    /// <summary>
    /// The datalayer for <see cref="Tag"/>
    /// </summary>
    public class TagDB : MongoTask<Entities.Tag>
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public TagDB():base(Entities.Tag.TableOrCollectionName)
        {

        }

        public IEnumerable<string> GetAllTagNames()
        {
            var projectionBuilder = Builders<Entities.Tag>.Projection;
            ProjectionDefinition<Entities.Tag, string> projection = projectionBuilder.Expression(t => t.TagName);
            FilterDefinitionBuilder<Entities.Tag> filterBuild = Builders<Entities.Tag>.Filter;
            FilterDefinition<Entities.Tag> filter = filterBuild.Empty;
            IEnumerable<string> tagNames = connector.Collection.Find(filter).Project(projection).ToEnumerable();
            return tagNames;
        }
    }
}
