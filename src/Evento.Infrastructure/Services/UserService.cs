using AutoMapper;
using Castle.Core.Resource;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.DTO;
using Evento.Infrastructure.Extensions;

namespace Evento.Infrastructure.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;
        private readonly string _passwordHashPepper;
        private readonly int _iteration = 3;

        public UserService(IUserRepository userRepository, IJwtHandler jwtHandler, IMapper mapper, string passwordHashPepper)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
            _passwordHashPepper = passwordHashPepper;
        }

        public async Task<AccountDto> GetAccountAsync(int userId)
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

            var passwordHash = PasswordHasher.ComputeHash(password, user.PasswordSalt, _passwordHashPepper, _iteration);

            if (user.Password != passwordHash)
            {
                throw new Exception($"Invalid credentials.");
            }

            var jwt = _jwtHandler.CreateToken(user.ID, user.Role);

            return new TokenDto
            {
                Token = jwt.Token,
                Expires = jwt.Expires,
                Role = user.Role,
            };
        }

        public async Task RegisterAsync(string username, string password, string email, string role = "user")
        {
            var user = await _userRepository.GetAsync(email);

            if (user != null) 
            {
                throw new Exception($"User with email '{email}' already exists!");
            }



            user = new User(role, username, email);
            user.SetPasswordHash(password, _passwordHashPepper, _iteration);

            await _userRepository.AddAsync(user);
        }

        public async Task ForgotPassword(string email)
        {
            var user = await _userRepository.GetAsync(email);

            if (user != null)
            {
                var token = ResetPasswordHandler.CreateToken(48);

                user.SetResetPasswordToken(token);
                await _userRepository.UpdateAsync(user);

                //TODO: wysyłanie emaila z linkiem
            }
        }

        public async Task ResetPassword(string token, string password)
        {
            var user = await _userRepository.GetByTokenAsync(token);

            if(user != null)
            {
                user.SetPasswordHash(password, _passwordHashPepper, _iteration);
                await _userRepository.UpdateAsync(user);
            }
        }

        public async Task PasswordTokenValidate()
        {
            ICollection<User> users = await _userRepository.GetResetPasswordUsers();
            List<User> updateUsers = new List<User>();

            foreach (User user in users)
            {
                if (!string.IsNullOrWhiteSpace(user.ResetPasswordToken) &&
                    user.ValidatePasswordToken())
                {
                    user.ClearResetPassword();
                    updateUsers.Add(user);
                }
            }

            if (updateUsers.Count > 0)
            {
                await _userRepository.UpdateRangeAsync(updateUsers);
            }
        }
    }
}
