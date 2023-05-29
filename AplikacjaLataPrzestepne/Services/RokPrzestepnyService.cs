using AplikacjaLataPrzestepne.Forms;
using AplikacjaLataPrzestepne.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace AplikacjaLataPrzestepne.Services
{
    public class RokPrzestepnyService : RokPrzestepnyInterface
    {
        private readonly Wyszukiwania _context;

        public RokPrzestepnyService(Wyszukiwania context)
        {
            _context = context;
        }

        public async Task<List<RokPrzestepny>> GetAllRokAsync()
        {
            return await _context.LeapData.ToListAsync();
        }

        public async Task<RokPrzestepny> GetRokByIdAsync(int id)
        {
            return await _context.LeapData.FindAsync(id);
        }

        public async Task CreateRokAsync(RokPrzestepny rok)
        {
            _context.LeapData.Add(rok);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRokAsync(RokPrzestepny rok)
        {
            _context.LeapData.Update(rok);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRokAsync(int id, RokPrzestepny r)
        {
            r = _context.LeapData.Find(id);
            
            _context.LeapData.Remove(r);
                await _context.SaveChangesAsync();
            
        }

    }
}
