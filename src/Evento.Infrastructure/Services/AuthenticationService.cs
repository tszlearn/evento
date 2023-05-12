using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.Commands.Authentication;
using Evento.Infrastructure.DTO;
using Evento.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _passwordHashPepper;
        private readonly IJwtHandler _jwtHandler;
        private readonly int _iteration = 3;

        public AuthenticationService(IUserRepository userRepository, IJwtHandler jwtHandler, string passwordHashPepper)
        {
            _userRepository = userRepository;
            _passwordHashPepper = passwordHashPepper;
            _jwtHandler = jwtHandler;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var user = await _userRepository.GetAsync(request.Email);
            if (user == null)
            {
                throw new Exception($"Invalid credentials.");
            }

            var passwordHash = PasswordHasher.ComputeHash(request.Password, user.PasswordSalt, _passwordHashPepper, _iteration);

            if (user.Password != passwordHash)
            {
                throw new Exception($"Invalid credentials.");
            }

            var jwt = _jwtHandler.CreateToken(user.ID, user.Role);

            return new AuthenticationResponse
            {
                Token = jwt.Token,
                Expires = jwt.Expires,
                Role = user.Role,
            };
        }

        public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
        {
            var user = await _userRepository.GetAsync(request.Email);

            if (user != null)
            {
                throw new Exception($"User with email '{request.Email}' already exists!");
            }



            user = new User(request.Role, request.UserName, request.Email);
            user.SetPasswordHash(request.Password, _passwordHashPepper, _iteration);

            await _userRepository.AddAsync(user);

            return new RegistrationResponse()
            {
                UserId = user.ID
            };
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userRepository.GetAsync(request.Email);

            if (user != null)
            {
                var token = ResetPasswordHandler.CreateToken(48);

                user.SetResetPasswordToken(token);
                await _userRepository.UpdateAsync(user);

                //TODO: wysyłanie emaila z linkiem
            }

            return new ResetPasswordResponse();
        }

        public async Task<ForgotPasswordResponse> SignForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var user = await _userRepository.GetByTokenAsync(request.Token);

            if (user != null)
            {
                user.SetPasswordHash(request.Password, _passwordHashPepper, _iteration);
                await _userRepository.UpdateAsync(user);
            }

            return new ForgotPasswordResponse();
        }
    }
}
