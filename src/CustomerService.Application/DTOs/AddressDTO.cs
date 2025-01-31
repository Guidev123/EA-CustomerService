using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application.DTOs;

public record AddressDTO(string Street, string Number, string AdditionalInfo,
                         string Neighborhood, string ZipCode, string City,
                         string State);