using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonPaar.Models
{
    public interface IStoringContents<T> where T : class
    {
        public Dictionary<string, List<T>> AppStorages { get; set; }
    }
}
