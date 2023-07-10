using AntonPaar.Models.ReadingFiles;
using AntonPaar.ProcessData.ReadingFiles;
using System.Text;
using System.Text.RegularExpressions;

namespace AntonPaar.ProcessData.ReadingFiles
{
    /// <summary>
    /// This class is intended to support multiple source of data.
    /// The data can be provided by any source.
    /// if the business implementation remains same for all the data, then we can use this class.
    /// Else, we can create a new Class and inherit from "IProcessStreamReaderComponent".
    /// The new class will have the implementation of business logic for specific source of data.
    /// Example - This will support various source of data such as Text file, Log FIle, CSV.
    /// But if tomorrow, business implementation changes for LOG Files, then we create a new class for
    /// Log file.
    /// By doing it we dont break the implentation of Rest of the files.
    /// We only implement for LOG files
    /// By doing this, we assure, not much of risks, saves time for development and testing is also reduced.
    /// </summary>
    public class ReadFileProcessingComponent : IProcessStreamReaderComponent
    {
        
        private StringBuilder _fileContent
        {
            get;
            set;
        }

        private string _filePathWithName
        {
            get; set;
        }

        private ReadingFileHelper? _helper
        {
            get;set;
        }

        private CustomConfiguration _configuration
        {
            get;
            set;
        }

        public ReadFileProcessingComponent(string filePathWithName, CustomConfiguration customConfiguration)
        {
            _filePathWithName = filePathWithName;
            _configuration = customConfiguration;
        }

        public List<WordsCountModel>? GetFileResultModel()
        {
            List<WordsCountModel> ? fm = new List<WordsCountModel>();
            _helper = new ReadingFileHelper(_filePathWithName, _configuration);
            if(_helper.ValidateFile().IsValid == true)
            {
                fm = ReadFile();
            }
            return fm;
        }

        private List<WordsCountModel>? ReadFile()
        {
            if (_helper != null)
            {
                _fileContent= _helper.GetStream();
                if (_fileContent != null)
                {
                    //Replace the new lines, line breaks, tabs with whitespace(" "), so that we can split it and remove the white space from list 
                    Regex basicRegex = new Regex(_configuration.BaseRegexFiltersForTextFile);
                    _fileContent.Replace(_fileContent.ToString(), basicRegex.Replace(_fileContent.ToString(), " "));

                    //Generate the list of each word. These words matches the pattern we want
                    Regex wordRegex = new Regex(_configuration.WordRegexFiltersForTextFile);
                    List<string> x = _fileContent.ToString().Split(_configuration.WordSeperatorForTextFile).ToList();
                    List<string> listOfWords = _fileContent.ToString().Split(Convert.ToChar(_configuration.WordSeperatorForTextFile)).
                                                Where(word => string.IsNullOrEmpty(word) == false &&
                                                wordRegex.IsMatch(word)).ToList();

                    return (from word in listOfWords
                            group word by word into tempcountWord
                            let count = tempcountWord.Count()
                            orderby count descending
                            select new WordsCountModel
                            {
                                WordsCount = count,
                                DistinctWords = tempcountWord.Key
                            }).ToList();

                }
                else
                    return null;
            }
            else
                return null;
        }

    }
}
