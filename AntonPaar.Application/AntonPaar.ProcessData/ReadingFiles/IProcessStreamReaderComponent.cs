using AntonPaar.Models.ReadingFiles;

namespace AntonPaar.ProcessData.ReadingFiles
{
    /// <summary>
    /// This interface will only extract the data as given by the business.
    /// </summary>
    internal interface IProcessStreamReaderComponent
    {
        /// <summary>
        /// Read the stream and get the result that is supposed to be displayed on the UI
        /// This method is responsible to implement the all the business logic to be executed.
        /// It is not responsible for the source of data.
        /// Source of data should be passed to the method using the class
        /// </summary>
        internal List<WordsCountModel>? GetFileResultModel();
    }
}
