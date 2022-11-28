using HotelApp.Data;
using HotelApp.Models;
using HotelApp.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HotelApp.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public BookingController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IActionResult Index()
        {

            List<SchedulerGroupModel> roomData = new List<SchedulerGroupModel>();
            foreach (Room room in _db.Rooms)
            {
                roomData.Add(_mapper.Map<SchedulerGroupModel>(room));
            }
            ViewBag.GroupData = roomData;

            List<SchedulerItemModel> sourceData = new List<SchedulerItemModel>();
            foreach (Reservation res in _db.Reservations)
            {
                sourceData.Add(_mapper.Map<SchedulerItemModel>(res));
            }
            ViewBag.ItemData = sourceData;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult IndexPost(Reservation res)
        {
            if (ModelState.IsValid)
            {
                var userName = HttpContext.User.Identity.Name;
                var user = _db.Users.FirstOrDefault(u=>u.Email==userName);
                res.UserId = user.Id;
                _db.Reservations.Add(res);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(res);
        }
        [Authorize]
        public IActionResult List()
        {
            var userName = HttpContext.User.Identity.Name;
            var user = _db.Users.FirstOrDefault(u => u.Email == userName);
            IEnumerable<Reservation> res =_db.Reservations.Where(r => r.UserId == user.Id);
            return View(res);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            Reservation obj = _db.Reservations.Find(id);
            if (obj == null)
                return NotFound();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Reservations.Find(id);
            if (obj == null)
                return NotFound();
            _db.Reservations.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult AdminList()
        {
            var adminRes = new List<AdminReservationModel>();
            var reservations = _db.Reservations.Include(r => r.User);
            foreach (Reservation r in reservations) {
                adminRes.Add(_mapper.Map<AdminReservationModel>(r));
            }
            return View(adminRes);
        }
    }
}
