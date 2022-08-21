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

        public ListModel(Asp.NetCoreRazorFileUpload.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<FileModel> FileModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.FileModels != null)
            {
                FileModel = await _context.FileModels.ToListAsync();
            }
        }
    }
}
