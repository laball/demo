using Beisen.Survey.Domain;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Beisen.Survey.TestBase
{
    public class BeisenSurveyTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<SurveyTask, Guid> _appUserRepository;

        public BeisenSurveyTestDataSeedContributor(IRepository<SurveyTask, Guid> appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            /* Seed additional test data... */
            var task = new SurveyTask
            {
                Status = 1,
                SurveyName = "SurveyName-001",
                SurveyId = "SurveyId-001",
                AnswerId = "AnswerId-001",
                SurveyLink = "SurveyLink-001"
            };


            await _appUserRepository.InsertAsync(task);
        }
    }
}