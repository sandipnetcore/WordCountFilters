using AntonPaar.Models;
using AntonPaar.Models.ReadingFiles;
using AntonPaar.ProcessData.ReadingFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AntonPaar.ProcessData.ViewData
{
    /// <summary>
    /// This class is used to display the properties in the VIEW. it should not be used for any other purpose.
    /// We have "public" modifiers so that it can be exposed to the web projects.
    /// This class will set up communication between Web and Process.
    /// </summary>
    public class WordCountViewData
    {
        /// <summary>
        /// _pagenumber is used to display, which page user should be shown or 
        /// which page user has requested for.
        /// Default value is always 1.
        /// From View, the user types the page number in the Textbox.
        /// </summary>
        private int _pageNumber
        {
            get; set;
        }

        /// <summary>
        /// _pageSize is used to display the total number of records to be displayed on grid.
        /// This is also used to count number of pages
        /// </summary>
        private int _pageSize
        {
            get; set;
        }

        /// <summary>
        /// File selected by the user
        /// </summary>
        private string _fileName
        {
            get;set;
        }

        /// <summary>
        /// Configuration done in the appsettings.json.
        /// We dont load again and again for the user.
        /// Its tells the directory name and other reading conditions.
        /// </summary>
        private CustomConfiguration _customConfiguration
        {
            get;set;
        }

        /// <summary>
        /// This is for caching the huge volume of data.
        /// </summary>
        private IStoringContents<WordsCountModel>? _contents
        {
            get;set;
        }

        public WordCountViewData(string fileName, CustomConfiguration customConfiguration, IStoringContents<WordsCountModel> storingContents, int pageSize, int pageNumber) 
        {
            _pageNumber = pageNumber;
            _pageSize = pageSize;
            _fileName = fileName;
            _customConfiguration = customConfiguration;
            _contents = storingContents;
        }

        public WordCountViewModel GetViewData()
        {
            WordCountViewModel viewModel = new WordCountViewModel();
            IProcessStreamReaderComponent reader =
                    new ReadFileProcessingComponent(
                                                    Path.Combine(_customConfiguration.FilesStorageLocation, _fileName), 
                                                    _customConfiguration
                                                    );

            List<WordsCountModel>? wordCountDTO = new List<WordsCountModel>();

            if (_contents != null)
            {
                if(_contents.AppStorages != null)
                {
                    if (_contents.AppStorages.ContainsKey(_fileName) == false)
                    {
                        try
                        {
                            _contents.AppStorages.Add(_fileName, reader.GetFileResultModel());
                        }
                        catch (Exception ex) { }
                    }
                }
                else
                {
                    _contents.AppStorages = new Dictionary<string, List<WordsCountModel>>();
                    _contents.AppStorages.Add(_fileName, reader.GetFileResultModel());
                }
                wordCountDTO = _contents.AppStorages[_fileName];
            }
            
            if(wordCountDTO != null )
            {
                viewModel.PageSize = _pageSize;
                viewModel.PageNumber = _pageNumber;
                int totalPages = wordCountDTO.Count/_pageSize;
                viewModel.TotalPages = wordCountDTO.Count % _pageSize == 0 ? totalPages : totalPages + 1;
                viewModel.listOfWordsCountModels = wordCountDTO.Select(w=>w).Skip(_pageSize * (_pageNumber-1)).Take(_pageSize).ToList();

            }
            return viewModel;
        }

    }
}
