using ReSkill.API.Models;
using Xunit;

namespace ReSkill.Tests
{
    public class SessionTests
    {
        [Fact]
        public void StudySession_Should_Have_Correct_Data()
        {
            var session = new StudySession
            {
                Id = 1,
                Topic = "IoT",
                DurationMinutes = 60,
                CreatedAt = DateTime.Now
            };

            Assert.Equal("IoT", session.Topic);
            Assert.Equal(60, session.DurationMinutes);
            Assert.True(session.IsCompleted); 
        }
    }
}