using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonPaar.Models.ReadingFiles
{
    /// <summary>
    /// This model/DTO is used to display data on the VIEW. so if we want, we can manipulate the properties over here as per the view.
    /// We dont need to expose our main DTO's over here, it is not required.
    /// This DTO is responsible for the final output. We get DTO from Database and then convert it into View DTO.
    /// THe DTO that is to be exposed to USer and is suffixed with "ViewModel"
    /// And this is the only model that gets communicated between "web" and "process data".
    /// </summary>
    public class WordCountViewModel
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public List<WordsCountModel>? listOfWordsCountModels { get; set; }
    }
}
