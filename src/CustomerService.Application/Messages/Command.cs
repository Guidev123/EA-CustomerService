using FluentValidation.Results;
using MediatR;

namespace CustomerService.Application.Messages
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        protected Command() => Timestamp = DateTime.Now;
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; } = null!; 
        public virtual bool IsValid() => throw new NotImplementedException();
    }
}
