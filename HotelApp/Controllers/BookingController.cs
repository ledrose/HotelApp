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
                    Color= "#ea7a57"
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
                    RoomId=res.RoomId
                });
            }
            ViewBag.datasources = sourceData;

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


                /*
                User us = db.Users.Include(u => u.Reservations).FirstOrDefault(u => u.Email == userName);
                foreach (Reservation r in us.Reservations)
                {
                    System.Console.WriteLine(r.ToString());
                    System.Console.WriteLine("Ok");
                }
                */
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
