using System;

namespace Orleans.Business
{
    public class MessageService : IMessageService
    {
        static readonly Random random = new Random();

        public string GetMessage()
        {
            return $"Hello {random.Next(100,999)}";
        }
    }
}
