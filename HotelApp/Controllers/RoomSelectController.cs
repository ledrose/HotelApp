using HotelApp.Data;
using HotelApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.Controllers
{
    public class RoomSelectController : Controller
    {
        private readonly AppDbContext _db;
        public RoomSelectController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
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
                _db.Rooms.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var obj = _db.Rooms.Find(id); 
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
            return RedirectToAction("Index");
        }
    }
}
