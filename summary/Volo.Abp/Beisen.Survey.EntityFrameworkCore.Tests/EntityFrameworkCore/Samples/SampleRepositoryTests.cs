using Beisen.Survey.Domain;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Beisen.Survey.EntityFrameworkCore.Tests.EntityFrameworkCore.Samples
{
    public class SampleRepositoryTests : BeisenSurveyEntityFrameworkCoreTestBase
    {
        private readonly IRepository<SurveyTask, Guid> _appUserRepository;

        public SampleRepositoryTests()
        {
            _appUserRepository = GetRequiredService<IRepository<SurveyTask, Guid>>();
        }

        [Fact]
        public async Task Demo()
        {
            var count = await _appUserRepository.GetCountAsync();
            Assert.True(count > 0);
        }
    }
}