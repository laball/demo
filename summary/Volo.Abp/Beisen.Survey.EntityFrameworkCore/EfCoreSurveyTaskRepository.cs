using Beisen.Survey.Domain;
using Beisen.Survey.EntityFrameworkCore.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Beisen.Survey.EntityFrameworkCore
{
    public class EfCoreSurveyTaskRepository
        : EfCoreRepository<SurveyDbContext, SurveyTask, Guid>,
        ISurveyTaskRepository
    {
        public EfCoreSurveyTaskRepository(
            IDbContextProvider<SurveyDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}