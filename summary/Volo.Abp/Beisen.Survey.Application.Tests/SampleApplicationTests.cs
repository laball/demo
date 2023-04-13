using Beisen.Survey.Application.Contracts;
using Volo.Abp.Application.Dtos;
using Xunit;

namespace Beisen.Survey.Application.Tests
{
    public class SampleApplicationTests : BeisenSurveyApplicationTestBase
    {
        private readonly ISurveyAppService _surveyAppService;

        public SampleApplicationTests()
        {
            _surveyAppService = GetRequiredService<ISurveyAppService>();
        }

        [Fact]
        public async Task Demo()
        {
            var dto = new PagedAndSortedResultRequestDto
            {
                SkipCount = 0,
                MaxResultCount = 10
            };

            var count = await _surveyAppService.GetListAsync(dto);
            Assert.True(count.TotalCount > 0);
        }
    }
}