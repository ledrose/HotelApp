using HotelApp.Data;
using HotelApp.Models;
using HotelApp.ViewModels;
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
        private AppDbContext db;

        public BookingController(AppDbContext _db)
        {
            db = _db;
        }

        public IActionResult Index(int Id)
        {

            List<RoomScheduleModel> roomData = new List<RoomScheduleModel>();
            IEnumerable<Room> rooms = db.Rooms;
            foreach (Room room in rooms)
            {
                roomData.Add(new RoomScheduleModel {
                    Id=room.Id,
                    Name=(room.Id.ToString()+" комната"),
                    Capacity=room.SpotNumber,
                    Type=(room.SpotNumber==1)? "Нормальная" : (room.SpotNumber == 2)?"Улучшенная":"Элитная",
                    PriceWeekends=room.PriceWeekends,
                    PriceWorkday = room.PriceWorkday,
                    Color = "#ea7a57"
                });;
            }
            ViewBag.RoomDatas = roomData;

            List<SourceScheduleModel> sourceData = new List<SourceScheduleModel>();
            IEnumerable<Reservation> reservations = db.Reservations;
            foreach (Reservation res in reservations)
            {
                sourceData.Add(new SourceScheduleModel
                {
                    Id=res.Id,
                    Subject = "Забронировано",
                    Description ="Description",
                    StartTime=res.StartTime,
                    EndTime=res.EndTime,
                    RoomId=res.RoomId,
                    IsBlock = true
                });
            }
            ViewBag.datasources = sourceData;
            ViewBag.newId = sourceData.Count()+1;

            ViewBag.ResourceNames = new string[] { "HotelRoom" };

            /*
            List<SourceScheduleModel> reservs = new List<SourceScheduleModel>();
            IEnumerable<Reservation> resList = db.Reservations;
            foreach (Reservation reserv in resList)
            {
                reservs.Add(new SourceScheduleModel {RoomId=reserv.RoomId, StartTime=reserv.StartTime,EndTime=reserv.EndTime,IsAllDay=false,Subject="Hey",Id=reserv.Id});
            }
            ViewBag.dataSource = reservs;

            List<RoomScheduleModel> rooms = new List<RoomScheduleModel>();
            IEnumerable<Room> roomList = db.Rooms;
            foreach (Room room in roomList)
            {
                rooms.Add(new RoomScheduleModel {Id=room.Id,Text=room.Id+" комната",Color= "#cb6bb2" });
            }
            ViewBag.rooms = rooms;
            */


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
                return RedirectToAction("Index", "RoomSelect");
            }
            return View(res);
        }
        [Authorize]
        public IActionResult List()
        {
            var userName = HttpContext.User.Identity.Name;
            var user = db.Users.FirstOrDefault(u => u.Email == userName);
            IEnumerable<Reservation> res =db.Reservations.Where(r => r.UserId == user.Id);
            return View(res);
        }

    }
}
