using GuDash.Common.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompetencesIntegrationTests
{
    public class TestMediator : IMediator
    {
        public List<IDomainEvent> PublishedEvents { get; private set; } = new List<IDomainEvent>();
        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            this.PublishedEvents.Add(notification as IDomainEvent);
            return Task.CompletedTask;
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            this.PublishedEvents.Add(notification as IDomainEvent);
            return Task.CompletedTask;

        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<object> Send(object request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
