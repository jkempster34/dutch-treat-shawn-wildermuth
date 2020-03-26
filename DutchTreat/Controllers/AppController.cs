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
        private readonly IMailService mailService;

        public AppController(IMailService mailService) // Startup.cs is set up so that whenever a IMailSerive is requested, we get a new NullMailService
        {
            this.mailService = mailService;
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
                mailService.SendMessage("jkempster34@gmail.com", model.Subject, $"From: {model.Name} - {model.Email}, " +
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
    }
}
