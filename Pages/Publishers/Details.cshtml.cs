﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dan_Alexia_Lab2.Data;
using Dan_Alexia_Lab2.Models;

namespace Dan_Alexia_Lab2.Pages.Publishers
{
    public class DetailsModel : PageModel
    {
        private readonly Dan_Alexia_Lab2.Data.Dan_Alexia_Lab2Context _context;

        public DetailsModel(Dan_Alexia_Lab2.Data.Dan_Alexia_Lab2Context context)
        {
            _context = context;
        }

        public Publisher Publisher { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers.FirstOrDefaultAsync(m => m.ID == id);
            if (publisher == null)
            {
                return NotFound();
            }
            else
            {
                Publisher = publisher;
            }
            return Page();
        }
    }
}
