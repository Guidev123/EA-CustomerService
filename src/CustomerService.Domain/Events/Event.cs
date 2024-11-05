﻿using MediatR;

namespace CustomerService.Domain.Events
{
    public abstract class Event : Message, INotification
    {
        public Event() => Timestamp = DateTime.Now;
        public DateTime Timestamp { get; }
    }
}