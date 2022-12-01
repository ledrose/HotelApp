using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelApp.Models;
using HotelApp.ViewModels;
using HotelApp.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AutoMapper;

namespace HotelApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public int Age { get; set; }
        
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}
