using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.Repositories;

namespace Evento.Tests.Repositories
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task when_adding_new_user_it_should_be_added_correctly_to_list()
        {
            var user = new User(Guid.NewGuid(), "user", "test_name", "test@email.com", "secret");
            IUserRepository repository = new UserRepository();

            await repository.AddAsync(user);

            var existingUser = await repository.GetAsync(user.Id);
            Assert.NotNull(existingUser);
            Assert.Equal(user, existingUser);
        }
    }
}
