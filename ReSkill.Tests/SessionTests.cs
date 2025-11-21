using ReSkill.API.Models;
using Xunit;

namespace ReSkill.Tests
{
    public class SessionTests
    {
        [Fact]
        public void StudySession_Should_Have_Correct_Data()
        {
            // Arrange (Preparação)
            var session = new StudySession
            {
                Id = 1,
                Topic = "IoT",
                DurationMinutes = 60,
                CreatedAt = DateTime.Now
            };

            // Act (Ação - neste caso simples, só verificação de propriedade)

            // Assert (Validação)
            Assert.Equal("IoT", session.Topic);
            Assert.Equal(60, session.DurationMinutes);
            Assert.True(session.IsCompleted); // Valor padrão é true
        }
    }
}