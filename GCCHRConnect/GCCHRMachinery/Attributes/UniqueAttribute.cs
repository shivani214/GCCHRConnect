using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCCHRMachinery.Attributes
{
    /// <summary>
    /// Can be applied to any property which needs to hold only unique values in database. Presently support for only MongoDb has been created
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class UniqueAttribute : Attribute
    {

    }
}
