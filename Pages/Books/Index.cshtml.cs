using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dan_Alexia_Lab2.Data;
using Dan_Alexia_Lab2.Models;

namespace Dan_Alexia_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Dan_Alexia_Lab2.Data.Dan_Alexia_Lab2Context _context;

        public IndexModel(Dan_Alexia_Lab2.Data.Dan_Alexia_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = default!;
        public BookData BookD { get; set; }
        public int BookID { get; set; }
        public int CategoryID { get; set; }

        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
        {
            BookD = new BookData();

            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = sortOrder == "author" ? "author_desc" : "author";

            CurrentFilter = searchString;

            BookD.Books = await _context.Books
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .Include(b => b.BookCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                BookD.Books = BookD.Books.Where(s => s.Author.FirstName.Contains(searchString)
                || s.Author.LastName.Contains(searchString)
                || s.Title.Contains(searchString));


                if (id != null)
                {
                    BookID = id.Value;
                    Book book = BookD.Books.Where(b => b.ID == id.Value).SingleOrDefault();

                    if (book != null)
                    {
                        BookD.Categories = book.BookCategories.Select(s => s.Category);
                    }
                }
                else
                {
                    Book = await _context.Books
                        .Include(b => b.Publisher)
                        .Include(b => b.Author)
                        .ToListAsync();
                }

                switch (sortOrder)
                {
                    case "title_desc": BookD.Books = BookD.Books.OrderByDescending(s => s.Title); break;
                    case "author_desc": BookD.Books = BookD.Books.OrderByDescending(s => s.Author.FullName); break;
                    case "author": BookD.Books = BookD.Books.OrderBy(s => s.Author.FullName); break;
                    default: BookD.Books = BookD.Books.OrderBy(s => s.Title); break;

                }
            }
        }
    }
}