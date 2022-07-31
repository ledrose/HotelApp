using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index(int Id)
        {
            return Content($"ID запроса {Id}");
        }
    }
}
