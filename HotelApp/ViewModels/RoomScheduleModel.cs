using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.ViewModels
{
    public class RoomScheduleModel
    {
        public int Id { set; get; }
        public String Name { get; set; }
        public string Color { set; get; }
        public int Capacity { get; set; }
        public String Type { get; set; }
        public float PriceWeekends { get; set; }
        public float PriceWorkday { get; set; }
    }

}
