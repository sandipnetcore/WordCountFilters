using AntonPaar.ProcessData.CommonInterfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AntonPaar.ProcessData.ReadingFiles
{
    /// <summary>
    /// This class is internal. We dont want to expose this to "WEB project". 
    /// As this is only used to provide help to the classes related to the files and files processing.
    /// </summary>
    internal class ReadingFileHelper
    {
        
        public FileInfo fileDetails
        {
            get;
            set;
        }

        private CustomConfiguration _customConfiguration
        {
            get;
            set;
        }
        public ReadingFileHelper(string FilePathWithFileName, CustomConfiguration customConfiguration)
        {
            fileDetails = new FileInfo(FilePathWithFileName);
            _customConfiguration = customConfiguration;
        }

        public IValidationResults ValidateFile()
        {
            IValidationResults results = new ReadFileValidationResults();
            if (fileDetails.Exists)
            {
                if (fileDetails.Length > _customConfiguration.FileMaxSize)
                {
                    results.IsValid = false;
                    results.ValidationErrorMessage = "Validation Failed: File size is greater than the size specified by the application";
                }
                else
                    results.IsValid = true;
            }
            else
            {
                results.IsValid = false;
                results.ValidationErrorMessage = "Validation Failed: File doesnt exists";
            }

            return results;
        }

        /// <summary>
        /// The stream will read till end at once.
        /// If we are sure that file is given "line by line", then we can use a set a definite number of line reads.
        /// But if the file contains only one line with 1 GB of data, then it is difficult to know which was the last part of the file that was read.
        /// We cannot use batches of stream, because it is difficult to know the starting point of bytes.
        /// Hence in one go, we need to stream the entire file and process data in batches.
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetStream()
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                StreamReader reader = new StreamReader(fileDetails.FullName);
                stringBuilder.Append(reader.ReadToEnd());
                reader.Close();
            }
            catch (Exception ex)
            {
                //log the exception
            }
            return stringBuilder;
        }

        
    }
}
