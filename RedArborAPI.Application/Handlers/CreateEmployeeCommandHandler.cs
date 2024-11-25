using MediatR;
using RedArborAPI.Application.Commands;
using RedArborAPI.Domain.Entities;
using RedArborAPI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArborAPI.Application.Handlers
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                CompanyId = request.CompanyId,
                CreatedOn = request.CreatedOn,
                DeletedOn = request.DeletedOn,
                Email = request.Email,
                Fax = request.Fax,
                Name = request.Name,
                Lastlogin = request.Lastlogin,
                Password = request.Password,
                PortalId = request.PortalId,
                RoleId = request.RoleId,
                StatusId = request.StatusId,
                Telephone = request.Telephone,
                UpdatedOn = request.UpdatedOn,
                Username = request.Username
            };

            await _employeeRepository.AddEmployeeAsync(employee);
            return employee.Id;
        }
    }
}
