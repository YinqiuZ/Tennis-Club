using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using alpha3.Models;
using System.Linq;
using System.Threading.Tasks;

namespace alpha3.Controllers
{
    public class MemberDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MemberDashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Coaches()
        {
            var coaches = await _context.Coaches.ToListAsync();
            return View(coaches);
        }

        public async Task<IActionResult> Schedules()
        {
            var userId = _userManager.GetUserId(User);
            var MemberSchedules = await _context.Schedules
                                    .Include(s => s.Coach)
                                    .ToListAsync();

            return View(MemberSchedules);
        }

        [HttpPost]
        public async Task<IActionResult> Enroll(int scheduleId)
        {
            var userId = _userManager.GetUserId(User);

            var enrollment = new MemberSchedule
            {
                ScheduleId = scheduleId,
                MemberId = userId
            };

            _context.MemberSchedules.Add(enrollment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Schedules");
        }
    }
}
