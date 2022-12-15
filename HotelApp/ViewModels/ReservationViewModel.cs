using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.ViewModels
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RoomId { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public float FullPrice { get; set; }
    }
}
