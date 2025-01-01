using FluentValidation.Results;
using SharedLib.Domain.Messages;

namespace EA.IntegrationEvents.Integration
{
    public class ResponseMessage : Message
    {
        public ResponseMessage(ValidationResult validationResult) => ValidationResult = validationResult;
        public ValidationResult ValidationResult { get; set; }
    }
}
