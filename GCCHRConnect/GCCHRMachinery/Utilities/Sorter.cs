using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCCHRMachinery.Utilities
{
    public  static class Sorter
    {
        /// <summary>
        /// Sorts the elements by the key. Values of the keys are not sorted and remain with their sorted key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        public static void Sort(string[] key, string[] values)
        {
            Array.Sort(key, values);
        }
    }
}
