using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbiteMQReceiver
{
    [Serializable]
    public class MQMessage
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
