using AutoMapper;
using Beisen.Survey.Application.Contracts;
using Beisen.Survey.Domain;

namespace Beisen.Survey.Application
{
    public class SurveyApplicationAutoMapperProfile : Profile
    {
        public SurveyApplicationAutoMapperProfile()
        {
            CreateMap<CreateUpdateSurveyTaskDto, SurveyTask>();
            CreateMap<SurveyTask, SurveyTaskDto>();
        }
    }
}