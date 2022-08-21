using Asp.NetCoreRazorFileUpload.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorFileUploads.Models;
using System.IO.Compression;

namespace RazorFileUploads.Pages.Files
{
    public class CreateModel : PageModel
    {
        private readonly IWebHostEnvironment webHostEnvironmentl;

        private readonly AppDbContext appDbContext;


        public CreateModel(IWebHostEnvironment _webHostEnvironment, AppDbContext _appDbContext)
        {
            this.webHostEnvironmentl = _webHostEnvironment;
            this.appDbContext = _appDbContext;
        }

        [BindProperty]
        public FileViewModel FileUploads { get; set; } = default!;
        public void OnGet()
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                //uploads file to folder
                if (FileUploads.formFile.Length > 0)
                {
                    string filePath = Path.Combine(webHostEnvironmentl.WebRootPath, "FileUploads", FileUploads.formFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await FileUploads.formFile.CopyToAsync(stream);
                    }
                }
                //uploads file to database
                using (var memoryStream = new MemoryStream())
                {
                    await FileUploads.formFile.CopyToAsync(memoryStream);
                    if (memoryStream.Length < 2097152)
                    {
                        var file = new FileModel()
                        {
                            FileName = FileUploads.formFile.FileName,
                            Data = memoryStream.ToArray()
                        };
                        await appDbContext.FileModels.AddAsync(file);
                        await appDbContext.SaveChangesAsync();
                        ViewData["message"] = "file uploaded succefully";
                        return RedirectToPage("/Files/Index");
                    }
                    else
                    {
                        ModelState.AddModelError("File", "The file is too large");

                    }
                }
            }

            return Page();
        }
    }
}
