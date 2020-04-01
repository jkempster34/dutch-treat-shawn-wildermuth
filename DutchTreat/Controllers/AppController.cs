using DutchTreat.Data.Entities;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly DutchContext _context;

        /*
         * MVC controllers request dependencies via constructors. These depoendencies are 
         * injected in the StartUp.cs ConfigureServices method.
         * 
         * StartUp.cs is set up so that whenever a IMailService is requested, we get a new NullMailService.
         */
        public AppController(IMailService mailService, DutchContext context)
        {
            _mailService = mailService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model) // Will map the name of the input fields in form to the properties of the ContactViewModel (case isn't crucial)
        {
            if (ModelState.IsValid)
            {
                _mailService.SendMessage("jkempster34@gmail.com", model.Subject, $"From: {model.Name} - {model.Email}, " +
                    $"Message {model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }
            else
            {
                // Show errors
            }
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }

        public IActionResult Shop()
        {
            var results = from product in _context.Products
                          orderby product.Category
                          select product;

            return View(results.ToList());
        }
    }
}
