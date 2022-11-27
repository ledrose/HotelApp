using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelApp.Models;
using HotelApp.ViewModels;

namespace HotelApp.Data
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Room, SchedulerGroupModel>()
                .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.SpotNumber))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (src.SpotNumber == 1) ? "Нормальная" : (src.SpotNumber == 2) ? "Комфорт" : "Люкс"))
                .ForMember(dest => dest.SubgroupStack, opt => opt.MapFrom(src => false));
            CreateMap<Reservation, SchedulerItemModel>()
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.Editable, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => "Забронировано"));
            CreateMap<RegisterModel,User>();
            CreateMap<Reservation, AdminReservationModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.User.Surname));
        }
    }
}
