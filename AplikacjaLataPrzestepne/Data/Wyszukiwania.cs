using AplikacjaLataPrzestepne.Forms;
using Microsoft.EntityFrameworkCore;
using System;

namespace AplikacjaLataPrzestepne.Data
{
    public class Wyszukiwania : DbContext
    {
        public Wyszukiwania(DbContextOptions options) : base(options) { }

        public DbSet<RokPrzestepny> LeapData { get; set; }

    }
}
