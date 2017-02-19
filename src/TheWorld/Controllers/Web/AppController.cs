using System;
using Microsoft.AspNetCore.Mvc;
using TheWorld.ViewModels;
using TheWorld.Services;
using Microsoft.Extensions.Configuration;
using TheWorld.Models;
using Microsoft.Extensions.Logging;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private ILogger<AppController> _logger;
        private IWorldRepository _repository;
        private IConfigurationRoot _config;
        private IMailService _mailService;

        public AppController(IMailService mailService, IConfigurationRoot config,
            IWorldRepository repository,
            ILogger<AppController> logger)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var data = _repository.GetAllTrips();

                return View(data);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get trips in Index page: {ex.Message}");
                return Redirect("/error");
            }
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact (ContactViewModel model)
        {
            if (model.Email.Contains("aol.com")) ModelState.
                    AddModelError("", "We don't support AOL addresses!");

            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "From TheWorld", model.Message);

                ModelState.Clear();

                ViewBag.UserMessage = "Message Sent!";
            }

            return View();
        }
    }
}
