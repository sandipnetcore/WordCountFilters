using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonPaar.Models
{
    public class StoringContent<T> : IStoringContents<T> where T : class
    {
        /// <summary>
        /// AppSorages contains data in the form of list.
        /// The reason of having list is that we can use the same key for same datatypes.
        /// </summary>
        public Dictionary<string, List<T>> AppStorages 
        { 
            get;
            set;
        }
    }
}
