using AplikacjaLataPrzestepne.Data;
using AplikacjaLataPrzestepne.Forms;
using ListLeapYears;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AplikacjaLataPrzestepne.Services;
using System.Configuration;

namespace AplikacjaLataPrzestepne.Pages
{
    public class ListModel : PageModel
    {
        public IEnumerable<RokPrzestepny> LeapYearList;
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration Configuration;
        private readonly RokPrzestepnyInterface _rokService;
        private readonly IHttpContextAccessor _contextAccessor;

        public ListModel(ILogger<IndexModel> logger, IConfiguration configuration, IHttpContextAccessor contextAccessor, Wyszukiwania context, RokPrzestepnyInterface rokService)
        {
            _logger = logger;
            Configuration = configuration;
            _contextAccessor = contextAccessor;
            _rokService = rokService;
        }
        public RokPrzestepny obiekt_doSzukania { get; set; } = new RokPrzestepny();
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<RokPrzestepny> LataPrzestepne { get; set; }
        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var user_id = _contextAccessor.HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                obiekt_doSzukania.user_id = user_id.Value;
                
            }
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            var lata = await _rokService.GetAllRokAsync();
            switch (sortOrder)
            {
                case "Date":
                    lata = lata.OrderBy(s => s.Data).ToList();
                    break;
                case "date_desc":
                    lata = lata.OrderByDescending(s => s.Data).ToList();
                    break;
                default:
                    lata = lata.OrderByDescending(s => s.Data).ToList();
                    break;
            }
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            
            var lataQueryable = lata.AsQueryable();
            var pageSize = Configuration.GetValue("PageSize", 20);
            LataPrzestepne = await PaginatedList<RokPrzestepny>.CreateAsync(lata, pageIndex ?? 1, pageSize);
            
        }
        public IActionResult OnPost(int id_User)
        {
            _rokService.DeleteRokAsync(id_User, obiekt_doSzukania);
           
            return RedirectToAction("Async");
        }
    }
}