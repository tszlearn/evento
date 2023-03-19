using AutoMapper;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.DTO;
using Evento.Infrastructure.Services;
using FluentAssertions;
using Moq;

namespace Evento.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task register_async_should_invoke_add_async_on_user_repository()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var jwtHandlerMock = new Mock<IJwtHandler>();
            var mapperMock = new Mock<IMapper>();
            var userService = new UserService(userRepositoryMock.Object, jwtHandlerMock.Object, mapperMock.Object);

            //Act
            await userService.RegisterAsync(Guid.NewGuid(), "user_test", "secure_test", "test@email.com");

            //Assert
            userRepositoryMock.Verify(x=>x.AddAsync(It.IsAny<User>()), Times.Once());
        }


        [Fact]
        public async Task when_invoking_get_account_async_it_should_invoke_get_async_on_user_repository()
        {
            //Arrange
            var user = new User(Guid.NewGuid(), "user", "test_name", "test@email.com", "secret");
            var returnAccountDto = new AccountDto()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Role = user.Role
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            var jwtHandlerMock = new Mock<IJwtHandler>();
            var mapperMock = new Mock<IMapper>();
            var userService = new UserService(userRepositoryMock.Object, jwtHandlerMock.Object, mapperMock.Object);

            mapperMock.Setup(x=>x.Map<AccountDto>(user)).Returns(returnAccountDto);
            userRepositoryMock.Setup(x=>x.GetAsync(user.Id)).ReturnsAsync(user);


            //Act
            var existingAccountDto = await userService.GetAccountAsync(user.Id);

            //Assert
            userRepositoryMock.Verify(x => x.GetAsync(user.Id), Times.Once());
            existingAccountDto.Should().NotBeNull();
            existingAccountDto.Email.Should().Be(user.Email);
        }
    }
}
