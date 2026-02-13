using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.database.AppDbContextModels;
using TodoApp.Dtos;

namespace TodoApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _db;
        private readonly PasswordHasher<TblUser> _passwordHasher;

        public AccountController(AppDbContext appDbContext)
        {
            _db = appDbContext;
            _passwordHasher = new PasswordHasher<TblUser>();
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> RegisterUI()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(UserDto _dto)
        {
            //if (_dto.PasswordHash != ConfirmPassword)
            //{
            //    ModelState.AddModelError("", "Passwords do not match");
            //    return View(_dto);
            //}
            var user = new TblUser
            {
                Username = _dto.Username
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, _dto.PasswordHash);
            _db.TblUsers.Add(user);
            await _db.SaveChangesAsync();
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("Username", user.Username);
            return RedirectToAction("Index", "Tasks");
        }

        [HttpGet]
        public async Task<IActionResult> LoginUI()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserDto _dto)
        {
            var user = await _db.TblUsers.FirstOrDefaultAsync(x => x.Username == _dto.Username);
            if (user == null)
                return Unauthorized();
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, _dto.PasswordHash);
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized();
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("Username", user.Username);

            return RedirectToAction("Index", "Tasks");
        }
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
