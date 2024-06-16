using Ordering.Domain.Abstractions;
using Ordering.Domain.Models;

namespace Ordering.Domain.Events;

public sealed record OrderUpdateEvent(Order order) : IDomainEvent;