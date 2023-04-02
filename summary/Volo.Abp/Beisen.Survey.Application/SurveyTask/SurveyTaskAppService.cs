using Beisen.Survey.Application.Contracts;
using Beisen.Survey.Domain;
using Volo.Abp.Application.Dtos;

namespace Beisen.Survey.Application
{
    public class SurveyTaskAppService : SurveAppService, ISurveyAppService
    {
        private readonly ISurveyTaskRepository _surveyTaskRepository;

        public SurveyTaskAppService(ISurveyTaskRepository surveyTaskRepository)
        {
            _surveyTaskRepository = surveyTaskRepository;
        }

        public async Task<SurveyTaskDto> CreateAsync(CreateUpdateSurveyTaskDto input)
        {
            var inputTask = ObjectMapper.Map<CreateUpdateSurveyTaskDto, SurveyTask>(input);
            var outputTask = await _surveyTaskRepository.InsertAsync(inputTask);
            return ObjectMapper.Map<SurveyTask, SurveyTaskDto>(outputTask);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _surveyTaskRepository.DeleteAsync(id);
        }

        public async Task<SurveyTaskDto> GetAsync(Guid id)
        {
            var task = await _surveyTaskRepository.GetAsync(id);
            var taskDto = ObjectMapper.Map<SurveyTask, SurveyTaskDto>(task);
            return taskDto;
        }

        public async Task<PagedResultDto<SurveyTaskDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var tasks = await _surveyTaskRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
            var taskDtos = tasks.Select(c => ObjectMapper.Map<SurveyTask, SurveyTaskDto>(c)).ToList();

            var totalCount = await _surveyTaskRepository.GetCountAsync();
            return new PagedResultDto<SurveyTaskDto>(totalCount, taskDtos);
        }

        public async Task<SurveyTaskDto> UpdateAsync(Guid id, CreateUpdateSurveyTaskDto input)
        {
            var task = await _surveyTaskRepository.GetAsync(id);
            task = ObjectMapper.Map(input, task);
            task = await _surveyTaskRepository.UpdateAsync(task);

            var taskDto = ObjectMapper.Map<SurveyTask, SurveyTaskDto>(task);
            return taskDto;
        }
    }
}