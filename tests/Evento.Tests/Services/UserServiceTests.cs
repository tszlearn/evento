using AutoMapper;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.Commands.Authentication;
using Evento.Infrastructure.DTO;
using Evento.Infrastructure.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Evento.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task register_async_should_invoke_add_async_on_user_repository()
        {
            //Arrange
            var pepper = "pepper";
            var userRepositoryMock = new Mock<IUserRepository>();
            var jwtHandlerMock = new Mock<IJwtHandler>();
            var mapperMock = new Mock<IMapper>();
            var authenticationService = new AuthenticationService(userRepositoryMock.Object, jwtHandlerMock.Object, pepper);


            //Act
            await authenticationService.RegisterAsync(new RegistrationRequest()
            {
                UserName = "user_test", 
                Password = "secure_test", 
                Email = "test@email.com"
            });

            //Assert
            userRepositoryMock.Verify(x=>x.AddAsync(It.IsAny<User>()), Times.Once());
        }


        [Fact]
        public async Task when_invoking_get_account_async_it_should_invoke_get_async_on_user_repository()
        {
            //Arrange
            var pepper = "pepper";
            var salt = PasswordHasher.GenerateSalt();
            var passwordHash = PasswordHasher.ComputeHash("secret", salt, pepper, 3);
            var user = new User( "user", "test_name", "test@email.com", salt, passwordHash);
            var returnAccountDto = new AccountDto()
            {
                Id = user.ID,
                Email = user.Email,
                Name = user.Name,
                Role = user.Role.ToString(),
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            var jwtHandlerMock = new Mock<IJwtHandler>();
            var mapperMock = new Mock<IMapper>();
            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object);

            mapperMock.Setup(x=>x.Map<AccountDto>(user)).Returns(returnAccountDto);
            userRepositoryMock.Setup(x=>x.GetAsync(user.ID)).ReturnsAsync(user);


            //Act
            var existingAccountDto = await userService.GetAccountAsync(user.ID);

            //Assert
            userRepositoryMock.Verify(x => x.GetAsync(user.ID), Times.Once());
            existingAccountDto.Should().NotBeNull();
            existingAccountDto.Email.Should().Be(user.Email);
        }
    }
}
