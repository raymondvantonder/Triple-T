using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TripleT.Application.Common.Authorization.Models;
using TripleT.Application.Common.Mappings;
using TripleT.Domain.Entities;

namespace TripleT.Application.User.Commands.Login
{
    public class LoginUserDto : IMapFrom<UserEntity>, IMapFrom<TokenDetails>
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public int TokenExpiresInSeconds { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserEntity, LoginUserDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.Id))
                .ForMember(x => x.Firstname, opt => opt.MapFrom(y => y.Firstname))
                .ForMember(x => x.Surname, opt => opt.MapFrom(y => y.Surname))
                .ForMember(x => x.Email, opt => opt.MapFrom(y => y.Email))
                .ForMember(x => x.Cellphone, opt => opt.MapFrom(y => y.Cellphone))
                .ForMember(x => x.DateOfBirth, opt => opt.MapFrom(y => y.DateOfBirth))
                .ForMember(x => x.Role, opt => opt.MapFrom(y => y.Role));

            profile.CreateMap<TokenDetails, LoginUserDto>()
                .ForMember(x => x.Token, opt => opt.MapFrom(y => y.Token))
                .ForMember(x => x.TokenExpiresInSeconds, opt => opt.MapFrom(y => y.ExpiresInSeconds));
        }
    }
}
