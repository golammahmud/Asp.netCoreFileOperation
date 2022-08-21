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
    public class DetailsModel : PageModel
    {
        private readonly Asp.NetCoreRazorFileUpload.Data.AppDbContext _context;

        public DetailsModel(Asp.NetCoreRazorFileUpload.Data.AppDbContext context)
        {
            _context = context;
        }

      public FileModel FileModel { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FileModels == null)
            {
                return NotFound();
            }

            var filemodel = await _context.FileModels.FirstOrDefaultAsync(m => m.Id == id);
            if (filemodel == null)
            {
                return NotFound();
            }
            else 
            {
                FileModel = filemodel;
            }
            return Page();
        }
    }
}
