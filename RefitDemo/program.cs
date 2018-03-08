using Refit;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RefitDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var gitHubApi = RestService.For<IUserApi>("http://localhost:5000/api");
            var result1 = gitHubApi.GetUser().Result;
            var result2 = gitHubApi.GetUsers().Result;

            System.GC.Collect();
        }
    }

    public interface IUserApi
    {
        [Get("/Refit/User")]
        Task<HttpResponse<User>> GetUser();

        [Get("/Refit/Users")]
        Task<HttpResponse<IEnumerable<User>>> GetUsers();
    }

    public class HttpResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }

    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}