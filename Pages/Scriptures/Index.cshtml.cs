using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; 
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;
using System.Linq.Dynamic.Core;

namespace MyScriptureJournal.Pages.Scriptures
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
        {
            _context = context;
           // SortOption = "date";
        }

        [BindProperty(SupportsGet = true)]
        public string SortOption { get; set;}
        //public List<Anotation> Book { get; set; } = new List<Anotation>();
        public IList<Anotation> Anotation { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Books { get; set; }
        [BindProperty(SupportsGet = true)]
        public string BookName { get; set; }

       

        public async Task OnGetAsync()
        {
            IQueryable<Anotation> books = from m in _context.Scriptures
                                          select m;

            if (!string.IsNullOrEmpty(BookName))
            {
                books = books.Where(x => x.Book == BookName);
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                books = books.Where(s => s.Record.Contains(SearchString));
            }

            //System.Diagnostics.Debug.WriteLine($"SortOption passed: {SortOption}");
            // Apply sorting
            switch (SortOption)
            {
                case "book":
                    books = books.OrderBy(s => s.Book);
                    break;
                case "date":
                    books = books.OrderBy(s => s.Date);
                    break;
               // default:
                  //  books = books.OrderBy(s => s.Book);
                  //  break;
            }

            Anotation = await books.ToListAsync();
            Books = new SelectList(await books.Select(m => m.Book).Distinct().ToListAsync());
        }

    }
    }


/*
IQueryable<string> bookQuery = from m in _context.Scriptures
                                            orderby m.Book
                                            select m.Book;

            var books = from m in _context.Scriptures
                        select m;

            if (!string.IsNullOrEmpty(BookName))
            {
                books = books.Where(x => x.Book == BookName);
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                books = books.Where(predicate: s => s.Record.Contains(SearchString));
            }
            Anotation = await books.ToListAsync();
            Books = new SelectList(await bookQuery.Distinct().ToListAsync());

            //To sort
            
            Anotation = await books.ToListAsync();
            Books = new SelectList(await bookQuery.Distinct().ToListAsync()); 
 */