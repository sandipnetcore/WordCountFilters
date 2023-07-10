using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonPaar.ProcessData.ReadingFiles
{
    public enum SupportingFileTypes
    {
        [Description("TextFile")]
        TextFile = 1,
        [Description("LogFile")]
        LogFile = 2,
        [Description("CSVFile")]
        CSVFile = 3
    }
}
