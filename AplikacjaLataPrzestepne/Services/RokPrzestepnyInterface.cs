using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AplikacjaLataPrzestepne.Forms;
namespace AplikacjaLataPrzestepne.Services
{
    public interface RokPrzestepnyInterface
    {
        
            Task<List<RokPrzestepny>> GetAllRokAsync();
            Task<RokPrzestepny> GetRokByIdAsync(int id);
            Task CreateRokAsync(RokPrzestepny rok);
            Task UpdateRokAsync(RokPrzestepny rok);
            Task DeleteRokAsync(int id, RokPrzestepny r);
       
    }
}
