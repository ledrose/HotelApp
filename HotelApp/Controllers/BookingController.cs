using HotelApp.Data;
using HotelApp.Models;
using HotelApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EJ2CoreSampleBrowser.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HotelApp.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private AppDbContext db;

        public BookingController(AppDbContext _db)
        {
            db = _db;
        }

        public IActionResult Index(int Id)
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Reservation res)
        {
            if (ModelState.IsValid)
            {
                var userName = HttpContext.User.Identity.Name;
                var user = db.Users.FirstOrDefault(u=>u.Email==userName);
                res.UserId = user.Id;
                db.Reservations.Add(res);
                db.SaveChanges();
                /*
                IEnumerable<Reservation> view = db.Reservations.Include(r=>r.Room);
                foreach (Reservation r in view)
                {
                    var ro = r.Room;
                    var us = r.User;
                    System.Console.WriteLine("Ok");
                }
                */
                return RedirectToAction("Index", "RoomSelect");
            }
            return View(res);
        }
        


    }
}
