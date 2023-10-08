
using alpha3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace alpha3.Controllers
{
    [Authorize(Roles = "Member")]
    public class MemberController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MemberController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ViewSchedules()
        {
            var schedules = await _context.Schedules.ToListAsync();
            return View(schedules);
        }

        public async Task<IActionResult> ViewCoaches()
        {
            var coaches = await _context.Users.Where(u => u.UserType == UserType.Coach).ToListAsync();
            return View(coaches);
        }

    }
}