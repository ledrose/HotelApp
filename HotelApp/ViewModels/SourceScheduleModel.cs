using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.ViewModels
{
    public class SourceScheduleModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public String Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RoomId { get; set; }
    }
}
