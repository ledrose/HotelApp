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
            CreateMap<Room, RoomScheduleModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Id.ToString() + " комната"))
                .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.SpotNumber))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (src.SpotNumber == 1) ? "Нормальная" : (src.SpotNumber == 2) ? "Комфорт" : "Люкс"))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => "#ea7a57"));
            CreateMap<Reservation, SourceScheduleModel>()
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => "Забронировано"))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => "Description"))
                .ForMember(dest => dest.IsBlock, opt => opt.MapFrom(src => true));
            CreateMap<RegisterModel,User>();
        }
    }
}
