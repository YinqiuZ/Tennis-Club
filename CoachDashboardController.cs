using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using alpha3.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace alpha3.Controllers
{
    [Authorize(Roles = "Coach")]
    public class CoachDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CoachDashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = _context.CoachProfiles.FirstOrDefault(p => p.UserId == user.Id);
            if (profile == null)
            {
                // Handle the case when there's no profile (e.g., redirect to create a profile)
                return RedirectToAction("CreateProfile");
            }
            return View(profile);
        }
        public IActionResult CreateProfile()
        {
            return View();
        }

        public async Task<IActionResult> Schedules()
        {
            var user = await _userManager.GetUserAsync(User);
            var schedules = _context.Schedules.Where(s => s.EventName== user.Id).ToList();
            return View(schedules);
        }

        public async Task<IActionResult> EnrolledMembers(int scheduleId)
        {
            var members = _context.MemberSchedules
                                  .Where(ms => ms.ScheduleId == scheduleId)
                                  .Select(ms => ms.Member)
                                  .ToList();
            return View(members);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProfile(CoachProfile profile)
        {
            var user = await _userManager.GetUserAsync(User);
            profile.UserId = user.Id;

            _context.CoachProfiles.Add(profile);
            await _context.SaveChangesAsync();

            return RedirectToAction("Profile");
        }

    }
}
