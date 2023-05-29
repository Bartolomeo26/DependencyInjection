using AplikacjaLataPrzestepne.Data;
using AplikacjaLataPrzestepne.Forms;
using ListLeapYears;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace AplikacjaLataPrzestepne.Pages
{
    public class ListModel : PageModel
    {
        public IEnumerable<RokPrzestepny> LeapYearList;
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration Configuration;
        private readonly Wyszukiwania _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public ListModel(ILogger<IndexModel> logger, Wyszukiwania context, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _context = context;
            Configuration = configuration;
            _contextAccessor = contextAccessor;
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
            DateSort = sortOrder == "Date" ? "date_asc" : "Date";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
             
            IQueryable<RokPrzestepny> uzytkownicy = from s in _context.LeapData.OrderByDescending(x=>x.Data) select s;
            
            var pageSize = Configuration.GetValue("PageSize", 20);
            LataPrzestepne = await PaginatedList<RokPrzestepny>.CreateAsync(
                uzytkownicy.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
        public IActionResult OnPost(int id_User)
        {    
            obiekt_doSzukania = _context.LeapData.Find(id_User);
            
            obiekt_doSzukania.Id = id_User;
            _context.LeapData.Remove(obiekt_doSzukania);
            _context.SaveChanges();
            
            return RedirectToAction("Async");
        }
    }
}