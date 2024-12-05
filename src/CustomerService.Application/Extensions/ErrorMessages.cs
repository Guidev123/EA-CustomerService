using System.ComponentModel;

namespace CustomerService.Application.Extensions
{
    public enum ErrorMessages
    {
        [Description("Error")]
        ERROR,
        [Description("Success")]
        SUCCESS,
        [Description("Customer not found")]
        CUSTOMER_NOT_FOUND,
        [Description("Fail to persist data")]
        FAIL_PERSIST_DATA,
        [Description("Customer already exists")]
        CUSTOMER_ALREADY_EXISTS
    }
}
