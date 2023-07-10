using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonPaar.ProcessData
{
    public class CustomConfiguration
    {
        public string FilesStorageLocation { get; set; }
        public long FileMaxSize { get; set; }
        public string BaseRegexFiltersForTextFile { get; set; }
        public string WordRegexFiltersForTextFile { get; set; }

        public string WordSeperatorForTextFile { get; set; }
    }
}
