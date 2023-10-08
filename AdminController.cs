
using alpha3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
namespace alpha3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult CreateSchedule()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule(Schedule schedule)
        {
            _context.Add(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction("ManageSchedules");
        }

        public async Task<IActionResult> ManageSchedules()
        {
            var schedules = await _context.Schedules.ToListAsync();
            return View(schedules);
        }

       

        public async Task<IActionResult> ViewLists()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }
    }
}