﻿using CustomerService.Application.Extensions;
using CustomerService.Application.Helpers;
using CustomerService.Domain.Repositories;
using MediatR;
using SharedLib.Domain.Messages;
using SharedLib.Domain.Responses;

namespace CustomerService.Application.Commands.Delete
{
    public class DeleteCustomerHandler(ICustomerRepository customerRepository)
               : CommandHandler, IRequestHandler<DeleteCustomerCommand, Response<DeleteCustomerCommand>>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        public async Task<Response<DeleteCustomerCommand>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id);
            if (customer is null)
            {
                AddError(request.ValidationResult, ErrorMessages.CUSTOMER_NOT_FOUND.GetDescription());
                return new(request, 404, ErrorMessages.ERROR.GetDescription(), GetAllErrors(request.ValidationResult));
            }
            customer.SetAsDeleted();
            _customerRepository.UpdateAsync(customer);

            var persistData = await _customerRepository.UnitOfWork.CommitAsync();
            if (!persistData)
            {
                AddError(request.ValidationResult, ErrorMessages.FAIL_PERSIST_DATA.GetDescription());
                return new(request, 400, ErrorMessages.ERROR.GetDescription(), GetAllErrors(request.ValidationResult));
            }

            return new(request, 204, ErrorMessages.SUCCESS.GetDescription());
        }
    }
}
