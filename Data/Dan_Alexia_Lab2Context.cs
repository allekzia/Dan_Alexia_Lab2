using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dan_Alexia_Lab2.Models;

namespace Dan_Alexia_Lab2.Data
{
    public class Dan_Alexia_Lab2Context : DbContext
    {
        public Dan_Alexia_Lab2Context (DbContextOptions<Dan_Alexia_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Dan_Alexia_Lab2.Models.Book> Books { get; set; } = default!;
        public DbSet<Dan_Alexia_Lab2.Models.Publisher> Publishers { get; set; } = default!;
        public DbSet<Dan_Alexia_Lab2.Models.Author> Authors { get; set; } = default!;
        public DbSet<Dan_Alexia_Lab2.Models.Category> Categories { get; set; } = default!;
        public DbSet<Dan_Alexia_Lab2.Models.Member> Member { get; set; } = default!;
        public DbSet<Dan_Alexia_Lab2.Models.Borrowing> Borrowing { get; set; } = default!;
    }
}
