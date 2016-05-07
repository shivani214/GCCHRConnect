using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCCHRMachinery.Entities
{
    /// <summary>
    /// For all entities which are to be maintained in a database. Only the root entity must implement this 
    /// </summary>
    interface IMaintainInMongoDb
    {
        string Id { get; set; }
    }
}
