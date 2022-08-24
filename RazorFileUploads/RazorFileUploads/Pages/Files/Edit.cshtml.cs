using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asp.NetCoreRazorFileUpload.Data;
using RazorFileUploads.Models;
using Microsoft.AspNetCore.Hosting;

namespace RazorFileUploads.Pages.Files
{
    public class EditModel : PageModel
    {
        private readonly Asp.NetCoreRazorFileUpload.Data.AppDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public EditModel(Asp.NetCoreRazorFileUpload.Data.AppDbContext context, IWebHostEnvironment _webHostEnvironment)
        {
            this._context = context;
            this.webHostEnvironment =_webHostEnvironment;
        }
        public string fileName { get; set; }

        [BindProperty]
        public FileModel fileModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FileModels == null)
            {
                return NotFound();
            }

            this.fileModel =  await _context.FileModels.FirstOrDefaultAsync(m => m.Id == id);
            if (fileModel == null)
            {
                return NotFound();
            }
         
            return Page();
        }

     
        public async Task<IActionResult> OnPostAsync(IFormFile formFile)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

           
            if (formFile.FileName != null)
            {
                if (fileModel.FileName != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "FileUploads", fileModel.FileName);
                    System.IO.File.Delete(filePath);
                }
                _context.Attach(fileModel).State = EntityState.Modified;
            }
            _context.Attach(fileModel).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("/List");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileModelExists(fileModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Edit");
        }

        private bool FileModelExists(int id)
        {
          return (_context.FileModels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
