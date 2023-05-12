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
        private readonly IMapper _mapper;


        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<AccountDto> GetAccountAsync(int userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);

            return _mapper.Map<AccountDto>(user);
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
