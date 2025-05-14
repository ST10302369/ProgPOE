using Microsoft.AspNetCore.Mvc;
using ProgPOE.Models;
using System.Diagnostics;

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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LandingPage()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}