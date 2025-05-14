using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgPOE.Data;
using ProgPOE.Models;
using ProgPOE.Helpers;
using System.Threading.Tasks;

namespace ProgPOE.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Username and password are required.");
                return View();
            }

            var user = await _context.Users
                .Include(u => u.Farmer)
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View();
            }

            // Verify the password
            bool isPasswordCorrect = PasswordHasher.VerifyPassword(password, user.Password);

            if (!isPasswordCorrect)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View();
            }

            // If password correct but not hashed (legacy password), update to hashed version
            if (PasswordHasher.NeedsUpgrade(user.Password))
            {
                user.Password = PasswordHasher.HashPassword(password);
                await _context.SaveChangesAsync();
            }

            // Set up the session
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            if (user.FarmerId.HasValue)
            {
                HttpContext.Session.SetInt32("FarmerId", user.FarmerId.Value);
            }

            user.LastLogin = DateTime.Now;
            await _context.SaveChangesAsync();

            // Redirect based on role
            if (user.Role == UserRoles.Farmer)
            {
                return RedirectToAction("Dashboard", "Farmer");
            }
            else
            {
                return RedirectToAction("Dashboard", "Employee");
            }
        }

        // GET: Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}