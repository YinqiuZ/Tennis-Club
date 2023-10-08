using alpha3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using alpha3.ViewModels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace alpha3.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly ApplicationDbContext _context;


        // System parameters for roles
        private const string AdminRole = "Admin";
        private const string MemberRole = "Member";
        private const string CoachRole = "Coach";

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
             ApplicationDbContext context) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel model)
        {
            ApplicationUser user = new()
            {
                UserName = model.Username,
                Email = model.Email,
                UserType = model.UserType,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ProfilePictureUrl = string.IsNullOrEmpty(model.ProfilePictureUrl)
                    ? "path_to_default_image.jpg"
                    : model.ProfilePictureUrl
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.UserType.ToString());
                if (model.UserType == UserType.Member)
                {
                    var member = new Member
                    {
                        Name = $"{model.FirstName} {model.LastName}",
                        Email = model.Email
                    };
                    _context.Members.Add(member);
                    await _context.SaveChangesAsync();
                }

                await _signInManager.SignInAsync(user, isPersistent: false);

                if (model.UserType.ToString() == AdminRole)
                {
                    return RedirectToAction("Index", "AdminDashboard");
                }
                else if (model.UserType.ToString() == MemberRole)
                {
                    return RedirectToAction("Index", "MemberDashboard");
                }
                else if (model.UserType.ToString() == CoachRole)
                {
                    return RedirectToAction("Index", "CoachDashboard");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            foreach (var modelStateValue in ViewData.ModelState.Values)
            {
                foreach (var error in modelStateValue.Errors)
                {
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {   foreach (var modelStateValue in ViewData.ModelState.Values)
{
    foreach (var error in modelStateValue.Errors)
    {
        System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
    }
}

                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Login succeeded for user: {Username}", model.Username);

                    var user = await _userManager.FindByNameAsync(model.Username);
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains(AdminRole))
                    {
                        return RedirectToAction("Index", "AdminDashboard");
                    }
                    else if (roles.Contains(MemberRole))
                    {
                        return RedirectToAction("Index", "MemberDashboard");
                    }
                    else if (roles.Contains(CoachRole))
                    {
                        return RedirectToAction("Index", "CoachDashboard");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    _logger.LogWarning("Login failed for user: {Username}", model.Username);
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View(model);
        }
    }
}
