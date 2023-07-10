using AntonPaar.ProcessData.ReadingFiles;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static AntonPaar.ProcessData.ReadingFiles.ReadingFileHelper;

namespace AntonPaar.ProcessData
{
    /// <summary>
    /// The intent of this class is to provide common methods.
    /// This class can be used by everyone.
    /// It must contain common function that can be used by any modules.
    /// </summary>
    public class CommonHelper
    {
        /// <summary>
        /// Returns list of files.
        /// This is a common function and can be used by everyone.
        /// It is only responsible to get files of specific type.
        /// It is not responsible, how the files will be used. For example it is not responsible for 
        /// reading or copying or deleting or moving the files. It will only fetch the file names in the
        /// directory
        /// </summary>
        /// <param name="storageLocation"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public List<string> GetListOfFiles(string storageLocation, SupportingFileTypes fileType)
        {
            string fileExtension = string.Empty;
            switch (fileType)
            {
                case SupportingFileTypes.CSVFile:
                    {
                        fileExtension = "*.csv";
                        break;
                    }
                case SupportingFileTypes.LogFile:
                    {
                        fileExtension = "*.log";
                        break;
                    }
                case SupportingFileTypes.TextFile:
                    {
                        fileExtension = "*.txt";
                        break;
                    }
                default: break;
            }
            return GetAllFilesFromTheDirectory(storageLocation,fileExtension);
        }

        private List<string> GetAllFilesFromTheDirectory(string storageLocation, string? filetype = null)
        {
            List<string> files = new List<string>();
            if (string.IsNullOrEmpty(storageLocation) == false)
                files = Directory.GetFiles(storageLocation, filetype).ToList();
            else
                files = Directory.GetFiles(storageLocation).ToList();

            List<string> result = new List<string>();

            foreach(string item in files)
                result.Add(item.Split(@"\")[item.Split(@"\").Length - 1]);

            return result;
        }
    }

    /// <summary>
    /// CommonHelperExtensions is just a static class and used to create only entension methods.
    /// </summary>
    public static class CommonHelperExtensions
    {
        /// <summary>
        /// Extension method - Get the enum using the description. We are using "Reflection" concepts
        /// </summary>
        /// <param name="enumItem"></param>
        /// <returns></returns>
        public static string GetDescriptionOfEnum(this Enum enumItem)
        {
            var item = enumItem.GetType().GetField(enumItem.ToString());
            if (Attribute.GetCustomAttribute(item, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }
            return string.Empty;
        }

        /// <summary>
        /// Extension Method to find enum value. This is more simplied form of getting the enum.
        /// We pass the type of Enumeration and the description of the item.
        /// It fetches us the enum
        /// Reason of doing this - We might have multiple enums in our application, so we have a common generic method to know
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumType"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T GetEnumValue<T>(this Type enumType, string description)
        {
            
            var result = (T)enumType.GetField(description.ToString()).GetRawConstantValue();
            return result;
        }
    }
}
