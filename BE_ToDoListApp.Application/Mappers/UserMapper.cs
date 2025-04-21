using AutoMapper;
using BE_ToDoListApp.Application.DTOs.UserDTO;
using BE_ToDoListApp.Application.Utils;
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
                .ForMember(des => des.Id, opt => opt.MapFrom(src => Guid.CreateVersion7()))
                .ForMember(des => des.Password, opt => opt.MapFrom(src => HashUtil.PasswordHash(src.Password)))
                .ForMember(des => des.Role, opt => opt.MapFrom(src => (byte)UserRoleEnum.NORMALUSER))
                .ForMember(des => des.Status, opt => opt.MapFrom(src => (byte)UserStatusEnum.ACTIVE))
                .ForMember(des => des.Gender, opt => opt.MapFrom(src => (byte)UserGenderEnum.MALE));

            CreateMap<User, UserDTO>()
                .ForMember(des => des.Role, opt => opt.MapFrom(src => EnumUtil.GetEnumName<UserRoleEnum>(src.Role)))
                .ForMember(des => des.Status, opt => opt.MapFrom(src => EnumUtil.GetEnumName<UserRoleEnum>(src.Status)))
                .ForMember(des => des.Gender, opt => opt.MapFrom(src => EnumUtil.GetEnumName<UserRoleEnum>(src.Gender)));

            CreateMap<UserDTO, User>()
                .ForMember(des => des.Role, opt => opt.MapFrom(src => EnumUtil.ParseEnum<UserRoleEnum>(src.Role)))
                .ForMember(des => des.Status, opt => opt.MapFrom(src => EnumUtil.ParseEnum<UserRoleEnum>(src.Status)))
                .ForMember(des => des.Gender, opt => opt.MapFrom(src => EnumUtil.ParseEnum<UserRoleEnum>(src.Gender)));
            ;
        }
    }
}
