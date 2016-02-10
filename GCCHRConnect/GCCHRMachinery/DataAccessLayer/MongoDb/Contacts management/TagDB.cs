using GCCHRMachinery.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCCHRMachinery.DataAccessLayer.MongoDb
{
    public class TagDB : MongoTask<Entities.Tag>
    {
        public TagDB():base(Entities.Tag.TableOrCollectionName)
        {

        }
    }
}
