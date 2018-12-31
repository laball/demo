using System.Threading.Tasks;
using Refit;

namespace CoreApp
{
    public interface IDpsClientService
    {
        [Get("/diantuo/api/services/Rcs/VntExport/Health")]
        [Headers("Authorization: Bearer")]
        Task<string> DiantuoHealth();
    }
}
