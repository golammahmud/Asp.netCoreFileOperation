using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Asp.NetCoreRazorFileUpload.Data;
using RazorFileUploads.Models;

namespace RazorFileUploads.Pages.Files
{
    public class ListModel : PageModel
    {
        private readonly Asp.NetCoreRazorFileUpload.Data.AppDbContext _context;

        private readonly IWebHostEnvironment webHostEnvironmentl;
        public ListModel(IWebHostEnvironment _webHostEnvironment, Asp.NetCoreRazorFileUpload.Data.AppDbContext context)
        {
            this._context = context;
            this.webHostEnvironmentl = _webHostEnvironment;
        }

        public IList<FileModel> FileModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.FileModels != null)
            {
                FileModel = await _context.FileModels.ToListAsync();
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
