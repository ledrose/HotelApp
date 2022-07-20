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
        
        public IActionResult Create()
        {   
            return View();
        }
    }
}
