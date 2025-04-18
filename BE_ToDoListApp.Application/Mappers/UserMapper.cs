﻿using AutoMapper;
using BE_ToDoListApp.Application.DTOs.UserDTO;
using BE_ToDoListApp.Domain.Entities;
using BE_ToDoListApp.Domain.Enums.UserEnums;
using BE_ToDoListApp.SharedLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Application.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<SignUpDTO, User>()
                .ForMember(des => des.Id, opt => opt.MapFrom(s => Guid.CreateVersion7()))
                .ForMember(des => des.Password, opt => opt.MapFrom(s => HashUtil.PasswordHash(s.Password)))
                .ForMember(des => des.Role, opt => opt.MapFrom(s => (byte)UserRoleEnum.NORMALUSER))
                .ForMember(des => des.Status, opt => opt.MapFrom(s => (byte)UserStatusEnum.ACTIVE))
                .ForMember(des => des.Gender, opt => opt.MapFrom(s => (byte)UserGenderEnum.MALE));
        }
    }
}
