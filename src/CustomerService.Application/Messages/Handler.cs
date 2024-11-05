using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application.Messages
{
    public abstract class Handler
    {
        protected Handler() => ValidationResult = new ValidationResult();
        protected ValidationResult ValidationResult;
        protected void AddError(string message) => ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
    }
}
