using MediatR;
using RedArborAPI.Application.Commands;
using RedArborAPI.Infrastructure.Interfaces;
using RedArborAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArborAPI.Application.Handlers
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository _repository;

        public UpdateEmployeeCommandHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetEmployeeByIdAsync(request.Id);
            if (employee == null)
            {
                throw new KeyNotFoundException("Employee not found");
            }

            // Actualizar los campos necesarios
            employee.CompanyId = request.CompanyId;
            employee.Name = request.Name;
            employee.Email = request.Email;
            employee.Fax = request.Fax;
            employee.Password = request.Password;
            employee.PortalId = request.PortalId;
            employee.RoleId = request.RoleId;
            employee.StatusId = request.StatusId;
            employee.Telephone = request.Telephone;
            employee.UpdatedOn  = DateTime.Now;
            employee.Username = request.Username;
            employee.StatusId = request.StatusId;

            await _repository.UpdateEmployeeAsync(employee);
            //return Unit.Value; // Unit es un valor vacío usado por MediatR
        }
    }
}
