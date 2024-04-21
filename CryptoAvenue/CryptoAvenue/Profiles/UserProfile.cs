using AutoMapper;
using CryptoAvenue.Dal.Dtos.UserDtos;
using CryptoAvenue.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAvenue.Dal.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserGetDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(d => d.Username, opt => opt.MapFrom(u => u.Username))
                .ForMember(d => d.Email, opt => opt.MapFrom(u => u.Email))
                .ForMember(d => d.Age, opt => opt.MapFrom(u => u.Age));
            CreateMap<UserGetDto, User>()
                .ForMember(d => d.Username, opt => opt.MapFrom(u => u.Username))
                .ForMember(d => d.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(d => d.Email, opt => opt.MapFrom(u => u.Email))
                .ForMember(d => d.Age, opt => opt.MapFrom(u => u.Age));
            CreateMap<User, UserPostDto>()
                .ForMember(d => d.Username, opt => opt.MapFrom(u => u.Username))
                .ForMember(d => d.Password, opt => opt.MapFrom(u => u.Password))
                .ForMember(d => d.Email, opt => opt.MapFrom(u => u.Email))
                .ForMember(d => d.Age, opt => opt.MapFrom(u => u.Age));
            CreateMap<UserPostDto, User>()
                .ForMember(d => d.Username, opt => opt.MapFrom(u => u.Username))
                .ForMember(d => d.Password, opt => opt.MapFrom(u => u.Password))
                .ForMember(d => d.Email, opt => opt.MapFrom(u => u.Email))
                .ForMember(d => d.Age, opt => opt.MapFrom(u => u.Age));
        }
    }
}
