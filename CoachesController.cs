
using alpha3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace alpha3.Controllers
{
    
    public class CoachesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoachesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ManageProfile()
        {
            var coach = await _context.Users.FindAsync(User.Identity.Name);
            return View(coach.CoachProfile);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(CoachProfile profile)
        {
            _context.Update(profile);
            await _context.SaveChangesAsync();
            return RedirectToAction("ManageProfile");
        }
        public IActionResult Index()
        {
            var coaches = _context.Coaches.ToList(); // Assuming you're using Entity Framework and `_context` is your DbContext
            return View(coaches);
        }



        public async Task<IActionResult> ViewEnrolledMembers(int scheduleId)
        {
            var schedule = await _context.Schedules
                .Include(s => s.EnrolledMembers)
                .FirstOrDefaultAsync(s => s.Id == scheduleId);
            return View(schedule.EnrolledMembers);
        }
    }
}