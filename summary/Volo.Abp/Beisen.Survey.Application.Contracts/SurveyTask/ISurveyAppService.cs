using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Beisen.Survey.Application.Contracts
{
    public interface ISurveyAppService :
        ICrudAppService< //Defines CRUD methods
        SurveyTaskDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateSurveyTaskDto> //Used to create/update a book
    {

    }
}