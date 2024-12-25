using DrugStore.Data.Entities.User;
using DrugStore.DTOs.User;
using DrugStore.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DrugStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AccountController(ILogger<AccountController> logger, IUserService userService, IRoleService roleService)
        {
            _logger = logger;
            _userService = userService;
            _roleService = roleService;
        }

        #region Register

        [Route("Register")]
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [Route("Register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            try
            {
                register.Email = register.Email.ToLower().Trim();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Email", e.Message);
                _logger.LogInformation("Error in Register: " + e.Message);
                return View(register);
            }

            if (await _userService.IsExistEmailAsync(register.Email))
            {
                ModelState.AddModelError("Email", $"{register.Email} is already taken");
                return View(register);
            }

            if (await _userService.IsExistUserNameAsync(register.Username))
            {
                ModelState.AddModelError("Username", $"{register.Username} is already taken");
                return View(register);
            }


            if (!register.RuleAccept)
            {
                ModelState.AddModelError("RuleAccept", "Please accept the site rules");
                return View(register);
            }




            User user = new User()
            {
                ActiveCode = UserDataGenerator.GeneratUniqCode(),
                Email = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName,
                IsEmailActive = true,
                RegisterDate = DateTime.Now,
                UserName = register.Username,
                Password = UserDataGenerator.PasswordHasher(register.Password),
                LastChange = DateTime.Now.AddMinutes(-3),
                SecurityCode = UserDataGenerator.GeneratUniqCode()
            };
            await _userService.AddUserAsync(user);

            _logger.LogInformation($"User Added! \n Username: {user.UserName} \n Email: {user.Email} \n FirstName: {user.FirstName} LastName: {user.LastName} /n");

            return Redirect("/Login");
        }
        #endregion

        #region Login
        [Route("Login")]

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login, string? returnUrl)
        {
            if (!ModelState.IsValid)
                return View(login);

            var user = await _userService.LoginUserAsync(login);
            if (user != null)
            {
                if (User.Identity.IsAuthenticated)
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.UserData, $"{user.FirstName} {user.LastName}"),
                            new Claim(ClaimTypes.Email, user.Email),
                        };

                // Get user roles and add them to claims
                var roles = _roleService.GetUserPermissions(user.UserName);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.PermissionId.ToString()));
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = login.RemindMe
                };

                await HttpContext.SignInAsync(principal, properties);
                _logger.LogInformation($"User Login! \n Username: {user.UserName} \n Email: {user.Email} \n FirstName: {user.FirstName} LastName: {user.LastName} \n Is Email Active? {user.IsEmailActive} \n Remember Me? {login.RemindMe}");

                if (returnUrl != null)
                    return Redirect(returnUrl);

                return Redirect("/");
            }
            ModelState.AddModelError("Email", "No user found with the given details");
            return View(login);
        }
        #endregion

        #region Logout

        public async Task<IActionResult> Logout(bool? EditProfile)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (EditProfile != null)
            {
                return Redirect("/Login?EditProfile=true");
            }
            return RedirectToAction("Login", "Account");
        }

        #endregion
    }
}
