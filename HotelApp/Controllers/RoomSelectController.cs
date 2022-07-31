using HotelApp.Data;
using HotelApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace HotelApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoomSelectController : Controller
    {
        private readonly AppDbContext _db;
        public RoomSelectController(AppDbContext db)
        {
            _db = db;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            IEnumerable<Room> objList = _db.Rooms;
            return View(objList);
        }

        public IActionResult List()
        {
            IEnumerable<Room> objList = _db.Rooms;
            return View(objList);
        }

        //get-room
        public IActionResult Create()
        {   
            return View();
        }

        //post -create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Room obj)
        {
            if (ModelState.IsValid) 
            {
                obj = ConvertImageToByte(obj);
                _db.Rooms.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var obj = _db.Rooms.Find(id);
            if (obj==null)
                return NotFound();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Room obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Image!=null)
                {
                    obj=ConvertImageToByte(obj);
                }
                _db.Rooms.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            Room obj = _db.Rooms.Find(id);
            if (obj == null)
                return NotFound();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Rooms.Find(id);
            if (obj == null)
                return NotFound();
            _db.Rooms.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    
        private Room ConvertImageToByte(Room room)
        {
            if (room.Image==null)
            {
                return room; //TODO
            }
            using (var target = new MemoryStream())
            {

                room.Image.CopyTo(target);
                room.ByteImage = Convert.ToBase64String(target.ToArray());
            }
            room.ContentType = room.Image.ContentType;
            room.SourceFileName= System.IO.Path.GetFileName(room.Image.FileName);
            return room;
        }
    }
}
