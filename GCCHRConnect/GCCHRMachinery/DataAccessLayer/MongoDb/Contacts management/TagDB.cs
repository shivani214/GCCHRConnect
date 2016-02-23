using GCCHRMachinery.Entities;


namespace GCCHRMachinery.DataAccessLayer.MongoDb
{
    /// <summary>
    /// The datalayer for <see cref="Tag"/>
    /// </summary>
    public class TagDB : MongoTask<Tag>
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public TagDB():base(Entities.Tag.TableOrCollectionName)
        {

        }
    }
}
