using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Streaker.DAL.Context;
using Streaker.DAL.Dtos.Streaks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Streaker.API.Tests.Streaks
{
    public class StreaksTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public StreaksTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_CheckStreaks()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/Streaks");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var streaks = await response.Content.ReadFromJsonAsync<List<StreakDto>>(); // Assuming StreakDto is your DTO class

            Assert.NotNull(streaks);
            Assert.Single(streaks); // Assuming you expect only one streak in the response

            var expectedStreak = streaks.First(); // Assuming you are expecting the first streak
            Assert.Equal("1", expectedStreak.Id);
            Assert.Equal("Sports", expectedStreak.Category);
            Assert.Equal("streak 1", expectedStreak.Name);
            Assert.Equal(60, expectedStreak.TargetCount);
        }
    }
}
