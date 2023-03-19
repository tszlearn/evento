using Evento.Infrastructure.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Net;

namespace Evento.Tests.EndToEnd.Controllers
{
    public class EventControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public EventControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task feching_events_should_return_not_empty_collection()
        {
            // Act
            var response = await _client.GetAsync("events");
            var content = await response.Content.ReadAsStringAsync();
            var events = JsonConvert.DeserializeObject<IEnumerable<EventDto>>(content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            events.Should().NotBeEmpty();
        }
    }
}
