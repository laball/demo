﻿namespace MassTransitService
{
    public class RabbitMqConstants
    {
        public const string RabbitMqUri = "rabbitmq://localhost/";
        public const string UserName = "guest";
        public const string Password = "guest";
        public const string GreetingQueue = "greeting.service";
        public const string HierarchyMessageSubscriberQueue = "hierarchyMessage.subscriber.service";
        public const string GreetingEventSubscriberAQueue = "greetingEvent.subscriberA.service";
        public const string GreetingEventSubscriberBQueue = "greetingEvent.subscriberB.service";

        public const string RequestClientQueue = "Request.Service";
    }
}