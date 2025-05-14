using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgPOE.Data;
using ProgPOE.Helpers;
using ProgPOE.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProgPOE.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employee/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            // Check if user is authenticated and is an employee
            if (HttpContext.Session.GetInt32("UserId") == null ||
                HttpContext.Session.GetString("Role") != "Employee")
            {
                return RedirectToAction("Login", "Account");
            }

            // Get counts for dashboard
            int farmerCount = await _context.Farmers.CountAsync();
            int productCount = await _context.Products.CountAsync();

            // Get product categories
            var categories = await _context.Products
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();

            // Get recent farmers
            var recentFarmers = await _context.Farmers
                .OrderByDescending(f => f.RegistrationDate)
                .Take(5)
                .ToListAsync();

            // Get recent products
            var recentProducts = await _context.Products
                .Include(p => p.Farmer)
                .OrderByDescending(p => p.ProductionDate)
                .Take(5)
                .ToListAsync();

            ViewBag.FarmerCount = farmerCount;
            ViewBag.ProductCount = productCount;
            ViewBag.Categories = categories;
            ViewBag.RecentFarmers = recentFarmers;
            ViewBag.RecentProducts = recentProducts;

            return View();
        }

        // GET: Employee/Farmers
        public async Task<IActionResult> Farmers()
        {
            // Check if user is authenticated and is an employee
            if (HttpContext.Session.GetInt32("UserId") == null ||
                HttpContext.Session.GetString("Role") != "Employee")
            {
                return RedirectToAction("Login", "Account");
            }

            var farmers = await _context.Farmers
                .OrderBy(f => f.LastName)
                .ToListAsync();

            return View(farmers);
        }

        // GET: Employee/CreateFarmer
        public IActionResult CreateFarmer()
        {
            // Check if user is authenticated and is an employee
            if (HttpContext.Session.GetInt32("UserId") == null ||
                HttpContext.Session.GetString("Role") != "Employee")
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // POST: Employee/CreateFarmer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFarmer([Bind("FirstName,LastName,Email,PhoneNumber,FarmName,FarmLocation")] Farmer farmer)
        {
            // Check if user is authenticated and is an employee
            if (HttpContext.Session.GetInt32("UserId") == null ||
                HttpContext.Session.GetString("Role") != "Employee")
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                farmer.RegistrationDate = DateTime.Now;
                _context.Add(farmer);
                await _context.SaveChangesAsync();

                // Generate username from email
                string username = farmer.Email.Split('@')[0].ToLower();

                // Default password
                string plainPassword = "farmer123";

                // Hash the password
                string hashedPassword = PasswordHasher.HashPassword(plainPassword);

                // Create a new user account for the farmer
                var user = new User
                {
                    Username = username,
                    Password = hashedPassword, // Store the hashed password
                    Role = UserRoles.Farmer,
                    FarmerId = farmer.Id,
                    RegistrationDate = DateTime.Now
                };

                _context.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Farmers));
            }
            return View(farmer);
        }

        // GET: Employee/EditFarmer/5
        public async Task<IActionResult> EditFarmer(int? id)
        {
            // Check if user is authenticated and is an employee
            if (HttpContext.Session.GetInt32("UserId") == null ||
                HttpContext.Session.GetString("Role") != "Employee")
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return NotFound();
            }

            var farmer = await _context.Farmers.FindAsync(id);
            if (farmer == null)
            {
                return NotFound();
            }

            return View(farmer);
        }

        // POST: Employee/EditFarmer/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFarmer(int id, [Bind("Id,FirstName,LastName,Email,PhoneNumber,FarmName,FarmLocation,RegistrationDate")] Farmer farmer)
        {
            // Check if user is authenticated and is an employee
            if (HttpContext.Session.GetInt32("UserId") == null ||
                HttpContext.Session.GetString("Role") != "Employee")
            {
                return RedirectToAction("Login", "Account");
            }

            if (id != farmer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(farmer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmerExists(farmer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Farmers));
            }
            return View(farmer);
        }

        // GET: Employee/FarmerProducts/5
        public async Task<IActionResult> FarmerProducts(int? id, string category = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            // Check if user is authenticated and is an employee
            if (HttpContext.Session.GetInt32("UserId") == null ||
                HttpContext.Session.GetString("Role") != "Employee")
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return NotFound();
            }

            var farmer = await _context.Farmers.FindAsync(id);
            if (farmer == null)
            {
                return NotFound();
            }

            // Get products with filtering
            var productsQuery = _context.Products.Where(p => p.FarmerId == id);

            // Apply filters if provided
            if (!string.IsNullOrEmpty(category))
            {
                productsQuery = productsQuery.Where(p => p.Category == category);
            }

            if (startDate.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ProductionDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ProductionDate <= endDate.Value);
            }

            // Get categories for filter dropdown
            var categories = await _context.Products
                .Where(p => p.FarmerId == id)
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();

            ViewBag.Farmer = farmer;
            ViewBag.Categories = new SelectList(categories);
            ViewBag.SelectedCategory = category;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            var products = await productsQuery
                .OrderByDescending(p => p.ProductionDate)
                .ToListAsync();

            return View(products);
        }

        // GET: Employee/AllProducts
        public async Task<IActionResult> AllProducts(string category = null, DateTime? startDate = null, DateTime? endDate = null, string farmerName = null)
        {
            // Check if user is authenticated and is an employee
            if (HttpContext.Session.GetInt32("UserId") == null ||
                HttpContext.Session.GetString("Role") != "Employee")
            {
                return RedirectToAction("Login", "Account");
            }

            // Start with all products
            var productsQuery = _context.Products.Include(p => p.Farmer).AsQueryable();

            // Apply filters if provided
            if (!string.IsNullOrEmpty(category))
            {
                productsQuery = productsQuery.Where(p => p.Category == category);
            }

            if (startDate.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ProductionDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ProductionDate <= endDate.Value);
            }

            if (!string.IsNullOrEmpty(farmerName))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Farmer.FirstName.Contains(farmerName) ||
                    p.Farmer.LastName.Contains(farmerName) ||
                    p.Farmer.FarmName.Contains(farmerName));
            }

            // Get categories for filter dropdown
            var categories = await _context.Products
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();

            // Get farmers for filter dropdown
            var farmers = await _context.Farmers
                .OrderBy(f => f.LastName)
                .ToListAsync();

            ViewBag.Categories = new SelectList(categories);
            ViewBag.Farmers = new SelectList(farmers, "Id", "FullName");
            ViewBag.SelectedCategory = category;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");
            ViewBag.FarmerName = farmerName;

            var products = await productsQuery
                .OrderByDescending(p => p.ProductionDate)
                .ToListAsync();

            return View(products);
        }

        private bool FarmerExists(int id)
        {
            return _context.Farmers.Any(e => e.Id == id);
        }
    }
}