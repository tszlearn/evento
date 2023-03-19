﻿using AutoMapper;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.DTO;
using Evento.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IJwtHandler jwtHandler, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
        }

        public async Task<AccountDto> GetAccountAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);

            return _mapper.Map<AccountDto>(user);
        }

        public async Task<TokenDto> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new Exception($"Invalid credentials.");
            }

            if(user.Password != password)
            {
                throw new Exception($"Invalid credentials.");
            }

            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);

            return new TokenDto
            {
                Token = jwt.Token,
                Expires = jwt.Expires,
                Role = user.Role,
            };
        }

        public async Task RegisterAsync(Guid userId, string username, string password, string email, string role = "user")
        {
            var user = await _userRepository.GetAsync(email);

            if (user != null) 
            {
                throw new Exception($"User with email '{email}' already exists!");
            }

            user = new User(userId, role, username, email, password);

            await _userRepository.AddAsync(user);
        }
    }
}
