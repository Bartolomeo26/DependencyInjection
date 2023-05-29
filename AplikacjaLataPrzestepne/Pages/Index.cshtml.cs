using AplikacjaLataPrzestepne.Data;
using AplikacjaLataPrzestepne.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace AplikacjaLataPrzestepne.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        [BindProperty]
        public RokPrzestepny rok_przestepny
        {
            get; set;
        } = new RokPrzestepny();

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly Wyszukiwania _context;

        public IndexModel(ILogger<IndexModel> logger, Wyszukiwania context, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _context = context;
            _contextAccessor = contextAccessor;
        }


        
        public void OnGet()
        {

            
            
        }

        public IActionResult OnPost()
        {

            this.rok_przestepny.Data = DateTime.Now;
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            { var user_id = _contextAccessor.HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                this.rok_przestepny.user_id = user_id.Value;
                this.rok_przestepny.login = _contextAccessor.HttpContext.User.Identity.Name;
            }


            string rok_przestepny;


            if ((this.rok_przestepny.Rok % 4 == 0 && this.rok_przestepny.Rok % 100 != 0) || this.rok_przestepny.Rok % 400 == 0)
            {
                rok_przestepny = "To był rok przestępny."; this.rok_przestepny.czy_przestepny = "rok przestępny";
            }
            else
            {
                rok_przestepny = "To nie był rok przestępny."; this.rok_przestepny.czy_przestepny = "rok nieprzestępny";
            }

            string result = this.rok_przestepny.Imie + " urodził się w " + this.rok_przestepny.Rok + " roku. " + rok_przestepny;
            string result1 = this.rok_przestepny.Rok + " rok. " + rok_przestepny;
            if (!string.IsNullOrEmpty(this.rok_przestepny.Imie) && !string.IsNullOrEmpty(this.rok_przestepny.Rok.ToString()))
            {
                ViewData["message"] = result;
                _context.LeapData.Add(this.rok_przestepny);
                _context.SaveChanges();

            }
            else if (!string.IsNullOrEmpty(this.rok_przestepny.Rok.ToString()))
            {
                ViewData["message"] = result1;
                _context.LeapData.Add(this.rok_przestepny);
                _context.SaveChanges();
            }
            

            return Page();


        }
    }
}