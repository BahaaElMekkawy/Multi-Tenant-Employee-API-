using Shared.Exceptions;

namespace EmployeeAPI.Exceptions
{
    public class EmployeeNotFoundException(Guid Id) : NotFoundException("employee",Id)
    {
    }
}
