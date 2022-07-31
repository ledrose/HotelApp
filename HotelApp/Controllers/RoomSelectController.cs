using HotelApp.Data;
using HotelApp.Models;
using HotelApp.ViewModels;
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
            IEnumerable<Room> objList = _db.Rooms.Include(u=>u.Image);
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
        public IActionResult Create(RoomViewModel obj)
        {
            if (ModelState.IsValid) 
            {
                var Image = ConvertImageToByte(obj.Image);
                _db.Images.Add(Image);
                _db.SaveChanges();

                Room room = new Room(){
                    PriceWeekends = obj.PriceWeekends,
                    PriceWorkday = obj.PriceWorkday,
                    SpotNumber = obj.SpotNumber,
                    Type = obj.Type,
                    Description= obj.Description,
                    ImageId = Image.Id,
                };
                _db.Rooms.Add(room);
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
            RoomViewModel roomView = new RoomViewModel()
            {
                Description = obj.Description,
                PriceWeekends = obj.PriceWeekends,
                PriceWorkday = obj.PriceWorkday,
                SpotNumber = obj.SpotNumber,
                Type = obj.Type,
                Id = obj.Id
            };
            return View(roomView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RoomViewModel obj)
        {
            if (ModelState.IsValid)
            {
                int imgId;
                var Image = ConvertImageToByte(obj.Image);
                if (Image!=null)
                {
                    _db.Images.Update(Image);
                    _db.SaveChanges();
                    imgId = Image.Id;
                }
                else
                {
                    imgId = _db.Rooms.Find(obj.Id).ImageId;
                }
                Room room = new Room()
                {
                    PriceWeekends = obj.PriceWeekends,
                    PriceWorkday = obj.PriceWorkday,
                    SpotNumber = obj.SpotNumber,
                    Type = obj.Type,
                    Description = obj.Description,
                    ImageId = imgId,
                    Id = obj.Id
                };
                _db.Rooms.Update(room);
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
            RoomViewModel roomView = new RoomViewModel()
            {
                Description = obj.Description,
                PriceWeekends = obj.PriceWeekends,
                PriceWorkday = obj.PriceWorkday,
                SpotNumber = obj.SpotNumber,
                Type = obj.Type,
                Id = obj.Id
            };
            return View(roomView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Rooms.Find(id);
            if (obj == null)
                return NotFound();
            var img = obj.Image;
            _db.Images.Remove(img);
            _db.Rooms.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    
        private Image ConvertImageToByte(IFormFile file)
        {
            if (file==null)
            {
                return null;
            }
            Image img = new Image();
            using (var target = new MemoryStream())
            {

                file.CopyTo(target);
                img.ByteImage=target.ToArray();
            }
            img.ContentType = file.ContentType;
            img.SourceFileName= System.IO.Path.GetFileName(file.FileName);
            return img;
        }
    }
}
