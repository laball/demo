using System;
using System.Threading.Tasks;

namespace Summary.Framework.Task
{
    public class AsyncTaskGeneric
    {
        public async Task<int> Test()
        {
            return await System.Threading.Tasks.Task.Factory.StartNew(GetInt);
        }

        public int GetInt()
        {
            var rd = new Random();
            return rd.Next(1, 100);
        }
    }
}