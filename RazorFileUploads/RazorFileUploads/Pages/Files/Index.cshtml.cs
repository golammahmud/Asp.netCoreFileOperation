using Asp.NetCoreRazorFileUpload.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorFileUploads.Models;
using System.IO.Compression;

namespace RazorFileUploads.Pages.Files
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment webHostEnvironmentl;

        private readonly AppDbContext appDbContext;
        public IndexModel(IWebHostEnvironment _webHostEnvironment, AppDbContext _appDbContext)
        {
            this.webHostEnvironmentl = _webHostEnvironment;
            this.appDbContext = _appDbContext;
        }
        public List<FileModel> fileList = new List<FileModel>();
        public void OnGet()
        {
            //Fetch all files in the Folder (Directory).
            string[] filePaths = Directory.GetFiles(Path.Combine(this.webHostEnvironmentl.WebRootPath, "FileUploads/"));

            //Copy File names to Model collection.
            //this.Files = new List<AppFileViewModel>();
            foreach (string filePath in filePaths)
            {
                this.fileList.Add(new FileModel { FileName = Path.GetFileName(filePath) });
            }
        }

        public FileResult OnGetDownloadFile(string fileName)
        {
            //Build the File Path.
            string path = Path.Combine(this.webHostEnvironmentl.WebRootPath, "FileUploads/") + fileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }
    }
}
