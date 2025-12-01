using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Models;
// «·≈÷«›«  «·÷—Ê—Ì… ·⁄„· „ÌÀÊœ About Ê«·≈Õ’«∆Ì« 
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models.SchoolViewModels;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // Œ«’Ì… ·Õ›Ÿ SchoolContext
        private readonly SchoolContext _context;

        // «·œ«·… «·»«‰Ì… (Constructor) ·Õﬁ‰ «· »⁄Ì… (Dependency Injection)
        public HomeController(ILogger<HomeController> logger, SchoolContext context)
        {
            _logger = logger;
            _context = context; //  ÂÌ∆… SchoolContext
        }

        // GET: Home/About
        // „ÌÀÊœ About ·⁄—÷ ≈Õ’«∆Ì«   Ã„Ì⁄ «·ÿ·«» Õ”»  «—ÌŒ «· ”ÃÌ·
        public async Task<ActionResult> About()
        {
            IQueryable<EnrollmentDateGroup> data =
                from student in _context.Students
                    // «· Ã„Ì⁄ Õ”»  «—ÌŒ «· ”ÃÌ·
                group student by student.EnrollmentDate into dateGroup
                // ≈‰‘«¡ ﬂ«∆‰ EnrollmentDateGroup ·ﬂ· „Ã„Ê⁄…
                select new EnrollmentDateGroup()
                {
                    EnrollmentDate = dateGroup.Key,
                    StudentCount = dateGroup.Count() // Õ”«» ⁄œœ «·ÿ·«» ›Ì ﬂ· „Ã„Ê⁄…
                };

            //  ‰›Ì– «·«” ⁄·«„ Ê≈—”«· «·»Ì«‰«  ≈·Ï «·‹ View
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}