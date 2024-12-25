using DrugStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var model = await _userService.FindUserWithUsernameAsync(User.Identity.Name);
            return View(model);
        }
    }
}
