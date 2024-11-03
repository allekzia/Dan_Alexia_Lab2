using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dan_Alexia_Lab2.Data;
using Dan_Alexia_Lab2.Models;
using Dan_Alexia_Lab2.Models.ViewModels;

namespace Dan_Alexia_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Dan_Alexia_Lab2.Data.Dan_Alexia_Lab2Context _context;

        public IndexModel(Dan_Alexia_Lab2.Data.Dan_Alexia_Lab2Context context)
        {
            _context = context;
        }

        public CategoryIndexData CategoryData { get; set; }
        public int CategoryID { get; set; }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync(int? id)
        {
            CategoryData = new CategoryIndexData();
            CategoryData.Categories = await _context.Categories
                .Include(c => c.BookCategories)
                    .ThenInclude(bc => bc.Book)
                    .ThenInclude(b => b.Author)
                .OrderBy(c => c.CategoryName)
                .ToListAsync();

            if (id != null)
            {
                CategoryID = id.Value;
                Category selectedCategory = CategoryData.Categories
                    .Where(c => c.ID == id.Value).Single();
                CategoryData.Books = selectedCategory.BookCategories
                    .Select(bc => bc.Book);
            }
        }
    }
}
