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
using Microsoft.AspNetCore.Mvc.Rendering;

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

            var userName = HttpContext.User.Identity.Name;
            var userId = _db.Users.FirstOrDefault(u => u.Email == userName).Id;

            List<SchedulerItemModel> resData = new List<SchedulerItemModel>();
            foreach (Reservation res in _db.Reservations)
            {
                var item = _mapper.Map<SchedulerItemModel>(res);
                if (item.UserId == userId) item.ClassName = "belongToUser";
                resData.Add(item);
            }
            ViewBag.ItemData = resData;
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
            IEnumerable<Reservation> reservations;
            if (HttpContext.User.IsInRole("admin"))
            {
                reservations = _db.Reservations.Include(r => r.User);
            }
            else {
                var userName = HttpContext.User.Identity.Name;
                var user = _db.Users.FirstOrDefault(u => u.Email == userName);
                reservations = _db.Reservations.Where(r => r.UserId == user.Id);
            }
            var res = new List<ReservationViewModel>();
            foreach (Reservation r in reservations)
            {
                res.Add(_mapper.Map<ReservationViewModel>(r));
            }
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

        
    }
}
