using AntonPaar.Models;
using AntonPaar.Models.ReadingFiles;
using AntonPaar.ProcessData;
using AntonPaar.ProcessData.ReadingFiles;
using AntonPaar.ProcessData.ViewData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AntonPaar.Web.Controllers
{
    public class ReadFileController : Controller
    {
        private readonly ILogger<ReadFileController> _logger;
        private readonly CustomConfiguration _customConfiguration;
        private IStoringContents<WordsCountModel> _storingContents;
        public ReadFileController(ILogger<ReadFileController> logger, IOptions<CustomConfiguration> configuration, IStoringContents<WordsCountModel> storingContents)
        {
            _logger = logger;
            _customConfiguration = configuration.Value;
            _storingContents = storingContents;
        }

        public IActionResult Index()
        {
            return View();
        }


        public ActionResult GetListOfFiles(string fileTypeSelected)
        {
            SupportingFileTypes supportingFileType = typeof(SupportingFileTypes).GetEnumValue<SupportingFileTypes>(fileTypeSelected);
            CommonHelper helper = new CommonHelper();
            var files = helper.GetListOfFiles(_customConfiguration.FilesStorageLocation, supportingFileType);
            return Json(files);
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            bool isUploadedSuccesfully = false;
            IFormFileCollection formFiles = HttpContext.Request.Form.Files;
            foreach(IFormFile file in formFiles)
            {
                string strFileNameAndLocation = Path.Combine(_customConfiguration.FilesStorageLocation + "/", file.Name);
                try
                {
                    using (var fileStream = new FileStream(strFileNameAndLocation, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                        isUploadedSuccesfully = true;
                    }
                }
                catch (Exception ex)
                {
                    return Json("Please configure FilesStorageLocation path in Appsettings.json!");
                }
            }
            
            if (isUploadedSuccesfully)
                return Json("File Uploaded Successfully!");
            else
                return Json("Failed to upload");
        }

        public ActionResult ReadFile()
        {
            if (HttpContext.Request.Headers.ContainsKey("fileSelected") == true)
            {
                int pageNumber = Convert.ToInt32(HttpContext.Request.Headers["pagenumber"].ToString());
                pageNumber = pageNumber == 0? 1: pageNumber;
                int pageSize = Convert.ToInt32(HttpContext.Request.Headers["pagesize"].ToString());
                pageSize = pageSize == 0? 100: pageSize;
                string fileName = HttpContext.Request.Headers["fileSelected"].ToString();
                WordCountViewData viewData = new WordCountViewData(fileName,_customConfiguration,_storingContents, pageSize, pageNumber);
                var results = viewData.GetViewData();     
                return Json(results);
            }
            else
            {
                return Json("File Name is not given");
            }
        }
    }
}