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

namespace Dan_Alexia_Lab2.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly Dan_Alexia_Lab2.Data.Dan_Alexia_Lab2Context _context;

        public IndexModel(Dan_Alexia_Lab2.Data.Dan_Alexia_Lab2Context context)
        {
            _context = context;
        }

        public IList<Publisher> Publisher { get;set; } = default!;

        public PublisherIndexData PublisherData { get; set; }
        public int PublisherID { get; set; }
        public int BookID { get; set; }

        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }


        public async Task OnGetAsync(int? id, int? bookID)
        {
            PublisherData = new PublisherIndexData(); 
            PublisherData.Publishers = await _context.Publishers
                .Include(i => i.Books).ThenInclude(c => c.Author)
                .OrderBy(i => i.PublisherName)
                .ToListAsync(); 
            
            if (id != null) 
            { 
                PublisherID = id.Value; 
                Publisher publisher = PublisherData.Publishers
                    .Where(i => i.ID == id.Value).Single(); 
                PublisherData.Books = publisher.Books; 
            }
        }

    }
}
