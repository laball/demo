using Xunit;

namespace Beisen.Survey.Domain.Tests
{
    public class SampleDomainTests : BeisenSurveyDomainTestBase
    {
        private readonly ISurveyTaskRepository _surveyTaskRepository;

        public SampleDomainTests()
        {
            _surveyTaskRepository = GetRequiredService<ISurveyTaskRepository>();
        }


        [Fact]
        public async Task Demo()
        {
            var count = await _surveyTaskRepository.GetCountAsync();
            Assert.True(count > 0);
        }
    }
}