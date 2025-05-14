using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgPOE.Data;
using ProgPOE.Helpers;
using ProgPOE.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Collections.Generic;

namespace ProgPOE.Controllers
{

    /*
--------------------------------Student Information----------------------------------
STUDENT NO.: ST10302369
Name: Lethabo Tyrell Penniston
Course: BCAD Year 3
Module: Programming 3A - ENTERPRISE APPLICATION DEVELOPMENT 
Module Code: PROG7311
Assessment: Portfolio of Evidence (POE) Part 2
Github repo link: https://github.com/ST10302369/ProgPOE
--------------------------------Student Information----------------------------------

==============================Code Attribution==================================

MVC APP
Author: Microsoft
Link: https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-9.0&tabs=visual-studio
Date Accessed: 08 May 2025

==============================Code Attribution==================================

*/
    public class FarmerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FarmerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Farmer/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            // Use direct session checks instead of extension methods for now
            if (HttpContext.Session.GetInt32("UserId") == null ||
                HttpContext.Session.GetString("Role") != "Farmer")
            {
                return RedirectToAction("Login", "Account");
            }

            int? farmerId = HttpContext.Session.GetInt32("FarmerId");
            if (farmerId == null)
            {
                return NotFound();
            }

            // Get farmer details
            var farmer = await _context.Farmers
                .FirstOrDefaultAsync(f => f.Id == farmerId);

            if (farmer == null)
            {
                return NotFound();
            }

            // Get farmer's products
            var products = await _context.Products
                .Where(p => p.FarmerId == farmerId)
                .ToListAsync();

            ViewBag.Farmer = farmer;
            ViewBag.Products = products;
            ViewBag.ProductCount = products.Count;
            ViewBag.Categories = products.Select(p => p.Category).Distinct().ToList();

            return View();
        }

        // GET: Farmer/Products
        public async Task<IActionResult> Products()
        {
            // Use direct session checks instead of extension methods for now
            if (HttpContext.Session.GetInt32("UserId") == null ||
                HttpContext.Session.GetString("Role") != "Farmer")
            {
                return RedirectToAction("Login", "Account");
            }

            int? farmerId = HttpContext.Session.GetInt32("FarmerId");
            if (farmerId == null)
            {
                ViewBag.Error = "No farmer ID found in session. Please log in again.";
                return View(new List<Product>());
            }

            // Get farmer's products
            var products = await _context.Products
                .Where(p => p.FarmerId == farmerId)
                .OrderByDescending(p => p.ProductionDate)
                .ToListAsync();

            return View(products);
        }

        // GET: Farmer/CreateProduct
        public IActionResult CreateProduct()
        {
            // Use direct session checks instead of extension methods for now
            if (HttpContext.Session.GetInt32("UserId") == null ||
                HttpContext.Session.GetString("Role") != "Farmer")
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // POST: Farmer/CreateProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct([Bind("Name,Category,ProductionDate,Quantity,Unit,PricePerUnit,Description,ImageUrl")] Product product)
        {
            // Debug information
            ViewBag.DebugInfo = new Dictionary<string, string>
            {
                { "Name", product.Name ?? "null" },
                { "Category", product.Category ?? "null" },
                { "ProductionDate", product.ProductionDate.ToString() },
                { "Quantity", product.Quantity.ToString() },
                { "Unit", product.Unit ?? "null" },
                { "PricePerUnit", product.PricePerUnit.ToString() },
                { "Description", product.Description ?? "null" },
                { "ImageUrl", product.ImageUrl ?? "null" },
                { "UserId", HttpContext.Session.GetInt32("UserId")?.ToString() ?? "null" },
                { "Role", HttpContext.Session.GetString("Role") ?? "null" },
                { "FarmerId", HttpContext.Session.GetInt32("FarmerId")?.ToString() ?? "null" }
            };

            // Print model state errors
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => new {
                    Property = x.Key,
                    Errors = x.Value.Errors.Select(e => e.ErrorMessage).ToList()
                }).Where(x => x.Errors.Count > 0).ToList();

                ViewBag.ModelStateErrors = errors;
            }

            // Use direct session checks instead of extension methods for now
            if (HttpContext.Session.GetInt32("UserId") == null ||
                HttpContext.Session.GetString("Role") != "Farmer")
            {
                ViewBag.AuthError = "Authentication failed: User is not authenticated as a Farmer";
                return View(product);
            }

            int? farmerId = HttpContext.Session.GetInt32("FarmerId");
            if (farmerId == null)
            {
                ViewBag.FarmerIdError = "Farmer ID not found in session. Please log in again.";
                return View(product);
            }

            // Set FarmerId before ModelState validation
            product.FarmerId = farmerId.Value;

            // Remove FarmerId validation errors if they exist
            if (ModelState.ContainsKey("FarmerId"))
            {
                ModelState.Remove("FarmerId");
            }
            if (ModelState.ContainsKey("Farmer"))
            {
                ModelState.Remove("Farmer");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Add default image URL if not provided
                    if (string.IsNullOrEmpty(product.ImageUrl))
                    {
                        // Set a default image based on the category
                        switch (product.Category?.ToLower())
                        {
                            case "vegetables":
                                product.ImageUrl = "https://via.placeholder.com/800x600.png?text=Vegetables";
                                break;
                            case "fruits":
                                product.ImageUrl = "https://via.placeholder.com/800x600.png?text=Fruits";
                                break;
                            case "dairy & eggs":
                                product.ImageUrl = "https://via.placeholder.com/800x600.png?text=Dairy+and+Eggs";
                                break;
                            case "meats":
                                product.ImageUrl = "https://via.placeholder.com/800x600.png?text=Meats";
                                break;
                            case "grains & cereals":
                                product.ImageUrl = "https://via.placeholder.com/800x600.png?text=Grains";
                                break;
                            case "herbs & spices":
                                product.ImageUrl = "https://via.placeholder.com/800x600.png?text=Herbs+and+Spices";
                                break;
                            default:
                                product.ImageUrl = "https://via.placeholder.com/800x600.png?text=Product";
                                break;
                        }
                    }

                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Product '{product.Name}' has been successfully created.";
                    return RedirectToAction(nameof(Products));
                }
                catch (Exception ex)
                {
                    ViewBag.DbError = $"Error saving product: {ex.Message}";
                    if (ex.InnerException != null)
                    {
                        ViewBag.DbInnerError = $"Inner error: {ex.InnerException.Message}";
                    }
                }
            }

            return View(product);
        }

        // GET: Farmer/EditProduct/5
        public async Task<IActionResult> EditProduct(int? id)
        {
            // Use direct session checks instead of extension methods for now
            if (HttpContext.Session.GetInt32("UserId") == null ||
                HttpContext.Session.GetString("Role") != "Farmer")
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return NotFound();
            }

            int? farmerId = HttpContext.Session.GetInt32("FarmerId");
            if (farmerId == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.FarmerId == farmerId);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Farmer/EditProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(int id, [Bind("Id,Name,Category,ProductionDate,Quantity,Unit,PricePerUnit,Description,FarmerId,ImageUrl")] Product product)
        {
            // Debug information
            ViewBag.DebugInfo = new Dictionary<string, string>
            {
                { "Id", product.Id.ToString() },
                { "Name", product.Name ?? "null" },
                { "Category", product.Category ?? "null" },
                { "ProductionDate", product.ProductionDate.ToString() },
                { "Quantity", product.Quantity.ToString() },
                { "Unit", product.Unit ?? "null" },
                { "PricePerUnit", product.PricePerUnit.ToString() },
                { "Description", product.Description ?? "null" },
                { "ImageUrl", product.ImageUrl ?? "null" },
                { "FarmerId", product.FarmerId.ToString() },
                { "UserId", HttpContext.Session.GetInt32("UserId")?.ToString() ?? "null" },
                { "Role", HttpContext.Session.GetString("Role") ?? "null" },
                { "FarmerIdSession", HttpContext.Session.GetInt32("FarmerId")?.ToString() ?? "null" }
            };

            // Print model state errors
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => new {
                    Property = x.Key,
                    Errors = x.Value.Errors.Select(e => e.ErrorMessage).ToList()
                }).Where(x => x.Errors.Count > 0).ToList();

                ViewBag.ModelStateErrors = errors;
            }

            // Use direct session checks instead of extension methods for now
            if (HttpContext.Session.GetInt32("UserId") == null ||
                HttpContext.Session.GetString("Role") != "Farmer")
            {
                ViewBag.AuthError = "Authentication failed: User is not authenticated as a Farmer";
                return View(product);
            }

            if (id != product.Id)
            {
                ViewBag.IdError = "ID mismatch error. The ID in the URL does not match the product ID.";
                return View(product);
            }

            int? farmerId = HttpContext.Session.GetInt32("FarmerId");
            if (farmerId == null)
            {
                ViewBag.FarmerIdError = "Farmer ID not found in session. Please log in again.";
                return View(product);
            }

            if (product.FarmerId != farmerId)
            {
                product.FarmerId = farmerId.Value;
            }

            // Remove FarmerId validation errors if they exist
            if (ModelState.ContainsKey("FarmerId"))
            {
                ModelState.Remove("FarmerId");
            }
            if (ModelState.ContainsKey("Farmer"))
            {
                ModelState.Remove("Farmer");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Add default image URL if not provided
                    if (string.IsNullOrEmpty(product.ImageUrl))
                    {
                        // Set a default image based on the category
                        switch (product.Category?.ToLower())
                        {
                            case "vegetables":
                                product.ImageUrl = "https://via.placeholder.com/800x600.png?text=Vegetables";
                                break;
                            case "fruits":
                                product.ImageUrl = "https://via.placeholder.com/800x600.png?text=Fruits";
                                break;
                            case "dairy & eggs":
                                product.ImageUrl = "https://via.placeholder.com/800x600.png?text=Dairy+and+Eggs";
                                break;
                            case "meats":
                                product.ImageUrl = "https://via.placeholder.com/800x600.png?text=Meats";
                                break;
                            case "grains & cereals":
                                product.ImageUrl = "https://via.placeholder.com/800x600.png?text=Grains";
                                break;
                            case "herbs & spices":
                                product.ImageUrl = "https://via.placeholder.com/800x600.png?text=Herbs+and+Spices";
                                break;
                            default:
                                product.ImageUrl = "https://via.placeholder.com/800x600.png?text=Product";
                                break;
                        }
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Product '{product.Name}' has been successfully updated.";
                    return RedirectToAction(nameof(Products));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!ProductExists(product.Id))
                    {
                        ViewBag.DbError = "Record not found. It may have been deleted by another user.";
                    }
                    else
                    {
                        ViewBag.DbError = $"Concurrency error: {ex.Message}";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.DbError = $"Error updating product: {ex.Message}";
                    if (ex.InnerException != null)
                    {
                        ViewBag.DbInnerError = $"Inner error: {ex.InnerException.Message}";
                    }
                }
            }

            return View(product);
        }

        // GET: Farmer/DeleteProduct/5
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            // Use direct session checks instead of extension methods for now
            if (HttpContext.Session.GetInt32("UserId") == null ||
                HttpContext.Session.GetString("Role") != "Farmer")
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return NotFound();
            }

            int? farmerId = HttpContext.Session.GetInt32("FarmerId");
            if (farmerId == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.FarmerId == farmerId);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Farmer/DeleteProduct/5
        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductConfirmed(int id)
        {
            // Use direct session checks instead of extension methods for now
            if (HttpContext.Session.GetInt32("UserId") == null ||
                HttpContext.Session.GetString("Role") != "Farmer")
            {
                return RedirectToAction("Login", "Account");
            }

            int? farmerId = HttpContext.Session.GetInt32("FarmerId");
            if (farmerId == null)
            {
                ViewBag.Error = "Farmer ID not found in session. Please log in again.";
                return RedirectToAction(nameof(Products));
            }

            try
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == id && p.FarmerId == farmerId);

                if (product == null)
                {
                    ViewBag.Error = "Product not found or you don't have permission to delete it.";
                    return RedirectToAction(nameof(Products));
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Product '{product.Name}' has been successfully deleted.";
                return RedirectToAction(nameof(Products));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error deleting product: {ex.Message}";
                return RedirectToAction(nameof(Products));
            }
        }

        // GET: Farmer/ProductDetail/5
        public async Task<IActionResult> ProductDetail(int? id)
        {
            // Use direct session checks instead of extension methods for now
            if (HttpContext.Session.GetInt32("UserId") == null ||
                HttpContext.Session.GetString("Role") != "Farmer")
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return NotFound();
            }

            int? farmerId = HttpContext.Session.GetInt32("FarmerId");
            if (farmerId == null)
            {
                ViewBag.Error = "Farmer ID not found in session. Please log in again.";
                return RedirectToAction(nameof(Products));
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.FarmerId == farmerId);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}