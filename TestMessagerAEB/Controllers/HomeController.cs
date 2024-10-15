using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestMessagerAEB.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using TestMessagerAEB.Data;
using Microsoft.EntityFrameworkCore;

namespace TestMessagerAEB.Controllers
{
    public class HomeController : Controller
    {
        private List<Message> _messages = new List<Message>();
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Send()
        {
            return View();
        }

        public IActionResult Realtime()
        {
            return View(_messages);
        }

        public IActionResult History(DateTime startTime, DateTime endTime)
        {
            var filteredMessages = _messages
                .Where(m => m.Time >= startTime && m.Time <= endTime)
                .ToList();

            return View(filteredMessages);
        }

        public IActionResult SendMessage(Message message)
        {
            message.Time = DateTime.Now;
            _context.messages.Add(message);
            _context.SaveChanges();

            return Json(new { success = true });
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
