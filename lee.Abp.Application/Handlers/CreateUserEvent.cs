using Abp.Events.Bus;

namespace Lee.Abp.Application.Handlers
{
    public class CreateUserEvent : EventData
    {
        public string Code { get; set; }
      
        public string Name { get; set; }
    }
}
