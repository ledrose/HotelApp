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

            List<RoomScheduleModel> roomData = new List<RoomScheduleModel>();
            foreach (Room room in _db.Rooms)
            {
                roomData.Add(_mapper.Map<RoomScheduleModel>(room));
            }
            ViewBag.RoomDatas = roomData;

            List<SourceScheduleModel> sourceData = new List<SourceScheduleModel>();
            foreach (Reservation res in _db.Reservations)
            {
                sourceData.Add(_mapper.Map<SourceScheduleModel>(res));
            }
            ViewBag.datasources = sourceData;
            ViewBag.newId = sourceData.Count + 1;
            ViewBag.ResourceNames = new string[] { "HotelRoom" };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Reservation res)
        {
            if (ModelState.IsValid)
            {
                var userName = HttpContext.User.Identity.Name;
                var user = _db.Users.FirstOrDefault(u=>u.Email==userName);
                res.UserId = user.Id;
                _db.Reservations.Add(res);
                _db.SaveChanges();
                return RedirectToAction("Index", "RoomSelect");
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
            return RedirectToAction("Index", "Home");
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
