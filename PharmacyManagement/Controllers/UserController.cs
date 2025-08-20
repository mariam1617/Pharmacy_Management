using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagement.Models;
using PharmacyManagement.ViewModel;

namespace PharmacyManagement.Controllers { 

    public class UserController : Controller
    {
        private readonly SignInManager<Staff> _signInManager;
        private readonly UserManager<Staff> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;


        public UserController(SignInManager<Staff> signInManager, UserManager<Staff> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel newUser)
        {
            if (ModelState.IsValid)
            {
                Staff? user = await _userManager.FindByNameAsync(newUser.Username);
                if (user != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(user, newUser.Password);
                    if (found)
                    {
                        await _signInManager.SignInAsync(user, newUser.RememberMe);
                        return RedirectToAction("Index", "Order");
                    }
                    else {
                        ModelState.AddModelError("password", "password is incorrect.");
                    }

                }
                else
                {
                    ModelState.AddModelError("username", "Username is incorrect.");

                }

            }
            return RedirectToAction("Index","Order");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel newUser)
        {
            if (ModelState.IsValid)
            {
                Staff user = new Staff();
                user.UserName = newUser.UserName;
                user.Email = newUser.email;
                IdentityResult Result = await _userManager.CreateAsync(user, newUser.Password);
                if (Result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Login");
                }
                foreach (var Error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, Error.Description);
                }
            }
            return View("Register", newUser);
        }


        [HttpGet]
        [Authorize(Roles = "admin")]

        public IActionResult AssignRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AssignRole(AssignRoleViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (!await _roleManager.RoleExistsAsync(model.RoleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(model.RoleName));
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            var result = await _userManager.AddToRoleAsync(user, model.RoleName);
            if (result.Succeeded)
            {

                ViewBag.Message = "Role assigned successfully.";


                return View(model);
            }

            return BadRequest(result.Errors);



        }

        [Authorize]
        public IActionResult MyRoles()
        {
            var roles = User.Claims
                .Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
            return Json(roles);
        }

    }
}
