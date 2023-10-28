using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.Scriptures
{
    public class DetailsModel : PageModel
    {
        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public DetailsModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
        {
            _context = context;
        }

      public Anotation Anotation { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Scriptures == null)
            {
                return NotFound();
            }

            var anotation = await _context.Scriptures.FirstOrDefaultAsync(m => m.Id == id);
            if (anotation == null)
            {
                return NotFound();
            }
            else 
            {
                Anotation = anotation;
            }
            return Page();
        }
    }
}
