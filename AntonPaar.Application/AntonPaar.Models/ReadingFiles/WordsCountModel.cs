using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonPaar.Models.ReadingFiles
{

    /// <summary>
    /// The intent of the class is, that we can extend the properties. Such as increasing the Columns in Db
    /// or Adding more fields in files or could be anything.
    /// It only contains the file information.
    /// Basically it is a kind of DTO. 
    /// For example, when we have databases items (result set from DB), then we expect this DTO models to be filled.
    /// This DTO, should not be exposed directly to the end user.
    /// Reason for saying this is because, we will use this DTO/model for computations and should not be exposed to end user.
    /// The DTOs that process DB/Files is suffixed with "Models".
    /// </summary>
    public class WordsCountModel
    {
        public int WordsCount { get; set; }

        public string? DistinctWords { get; set; }

    }
}
