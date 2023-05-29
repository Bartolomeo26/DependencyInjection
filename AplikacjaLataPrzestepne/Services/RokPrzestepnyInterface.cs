using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AplikacjaLataPrzestepne.Forms;
namespace AplikacjaLataPrzestepne.Services
{
    public interface RokPrzestepnyInterface
    {
        
            Task<List<RokPrzestepny>> GetAllPeopleAsync();
            Task<RokPrzestepny> GetPersonByIdAsync(int id);
            Task CreatePersonAsync(RokPrzestepny rok);
            Task UpdatePersonAsync(RokPrzestepny rok);
            Task DeletePersonAsync(int id);
       
    }
}
