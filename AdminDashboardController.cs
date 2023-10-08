using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using alpha3.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace alpha3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : Controller
    {
        private readonly ApplicationDbContext _context; // Assuming you have this dependency

        public AdminDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: To show the form
        [HttpGet]
        public IActionResult CreateSchedule()
        {
            // Fetch list of coaches for dropdown
            var coaches = _context.Coaches.ToList();
            ViewBag.Coaches = new SelectList(coaches, "CoachId", "Name");
            return View();
        }

        // POST: To handle form submission
        [HttpPost]
        public async Task<IActionResult> CreateSchedule(Schedule model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // or redirect wherever appropriate
            }
            return View(model);
        }

        // GET: Fetch and display all members
        public async Task<IActionResult> MembersList()
        {
            var members = await _context.Members.ToListAsync();
            return View(members);
        }
        public async Task<IActionResult> Index()
        {
            var members = await _context.Members.ToListAsync();
            return View(members);
        }

    }
}
