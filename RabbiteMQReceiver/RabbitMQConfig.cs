using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbiteMQReceiver
{

    public class RabbiteMQSection : ConfigurationSection
    {
        [ConfigurationProperty("Uri", DefaultValue = "")]
        public string Uri { get { return this["Uri"] as string; } }

        [ConfigurationProperty("Exchanges")]
        public ExchangElementCollection Exchanges { get { return (ExchangElementCollection)this["Exchanges"]; } }

        [ConfigurationProperty("Queues")]
        public QueueElementCollection Queues { get { return (QueueElementCollection)this["Queues"]; } }
    }

    [ConfigurationCollection(typeof(ExchangeElement), AddItemName = "Exchange", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class ExchangElementCollection : ConfigurationElementCollection
    {
        public ExchangeElement this[int index]
        {
            get { return (ExchangeElement)base.BaseGet(index); }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                base.BaseAdd(index, value);
            }
        }

        public new ExchangeElement this[string name]
        {
            get { return (ExchangeElement)base.BaseGet(name); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ExchangeElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as ExchangeElement).Exchange;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "Exchange"; }
        }
    }


    [ConfigurationCollection(typeof(ArgumentElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
    public class ArgumentElementCollection : ConfigurationElementCollection
    {
        public ArgumentElement this[int index]
        {
            get { return (ArgumentElement)base.BaseGet(index); }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }

                base.BaseAdd(index, value);
            }
        }

        public new ArgumentElement this[string name]
        {
            get { return (ArgumentElement)base.BaseGet(name); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ArgumentElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as ArgumentElement).Key;
        }
    }

    public class ArgumentElement : ConfigurationElement
    {
        [ConfigurationProperty("key", DefaultValue = "")]
        public string Key { get { return this["key"] as string; } }
        [ConfigurationProperty("value", DefaultValue = "")]
        public string Value { get { return this["value"] as string; } }
    }

    public class ExchangeElement : ConfigurationElement
    {
        [ConfigurationProperty("exchange", DefaultValue = "")]
        public string Exchange { get { return this["exchange"] as string; } }

        [ConfigurationProperty("type", DefaultValue = "")]
        public string Type { get { return this["type"] as string; } }

        [ConfigurationProperty("durable", DefaultValue = false)]
        public bool Durable { get { return bool.Parse(this["durable"].ToString()); } }

        [ConfigurationProperty("autoDelete", DefaultValue = false)]
        public bool AutoDelete { get { return bool.Parse(this["autoDelete"].ToString()); } }

        [ConfigurationProperty("Arguments")]
        public ArgumentElementCollection Arguments { get { return (ArgumentElementCollection)this["Arguments"]; } }

        [ConfigurationProperty("Bind")]
        public ExchangeBindElement Bind { get { return (ExchangeBindElement)this["Bind"]; } }
    }


    [ConfigurationCollection(typeof(QueueElement), AddItemName = "Queue", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class QueueElementCollection : ConfigurationElementCollection
    {
        public QueueElement this[int index]
        {
            get { return (QueueElement)base.BaseGet(index); }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                base.BaseAdd(index, value);
            }
        }

        public new QueueElement this[string name]
        {
            get { return (QueueElement)base.BaseGet(name); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new QueueElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as QueueElement).Queue;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "Queue"; }
        }
    }

    public class QueueElement : ConfigurationElement
    {
        [ConfigurationProperty("queue", DefaultValue = "")]
        public string Queue { get { return this["queue"] as string; } }

        [ConfigurationProperty("durable", DefaultValue = false)]
        public bool Durable { get { return bool.Parse(this["durable"].ToString()); } }

        [ConfigurationProperty("exclusive", DefaultValue = false)]
        public bool Exclusive { get { return bool.Parse(this["exclusive"].ToString()); } }

        [ConfigurationProperty("autoDelete", DefaultValue = false)]
        public bool AutoDelete { get { return bool.Parse(this["autoDelete"].ToString()); } }

        [ConfigurationProperty("Arguments")]
        public ArgumentElementCollection Arguments { get { return (ArgumentElementCollection)this["Arguments"]; } }

        [ConfigurationProperty("Bind")]
        public QueueBindElement Bind { get { return (QueueBindElement)this["Bind"]; } }
    }

    public class ExchangeBindElement : ConfigurationElement
    {
        [ConfigurationProperty("destination", DefaultValue = "")]
        public string Destination { get { return this["destination"] as string; } }

        [ConfigurationProperty("source", DefaultValue = "")]
        public string Source { get { return this["source"] as string; } }

        [ConfigurationProperty("routingKey", DefaultValue = "")]
        public string RoutingKey { get { return this["routingKey"] as string; } }

        [ConfigurationProperty("Arguments")]
        public ArgumentElementCollection Arguments { get { return (ArgumentElementCollection)this["Arguments"]; } }
    }


    public class QueueBindElement : ConfigurationElement
    {
        [ConfigurationProperty("queue", DefaultValue = "")]
        public string Queue { get { return this["queue"] as string; } }

        [ConfigurationProperty("exchange", DefaultValue = "")]
        public string Exchange { get { return this["exchange"] as string; } }

        [ConfigurationProperty("routingKey", DefaultValue = "")]
        public string RoutingKey { get { return this["routingKey"] as string; } }

        [ConfigurationProperty("Arguments")]
        public ArgumentElementCollection Arguments { get { return (ArgumentElementCollection)this["Arguments"]; } }
    }

    public class RabbitMQConfig
    {
        public const string DefaultVirtualHost = "/";
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
    }

    public class ExchangeConfig
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Durable { get; set; }
        public bool AutoDelete { get; set; }
        public IDictionary<string, object> Arguments { get; set; }
    }

    public class QueueConfig
    {
        public string Name { get; set; }
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
        public IDictionary<string, object> Arguments { get; set; }
    }
}
