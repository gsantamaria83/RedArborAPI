using MediatR;
using RedArborAPI.Application.Commands;
using RedArborAPI.Infrastructure.Interfaces;
using RedArborAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedArborAPI.Domain.Dtos;

namespace RedArborAPI.Application.Handlers
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeRepository _repository;

        public DeleteEmployeeCommandHandler(IEmployeeRepository repository)
        {
            _repository = repository;

        }

        public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetEmployeeByIdAsync(request.Id);
            if (employee == null)
            {
                throw new KeyNotFoundException("Employee not found");
            }

            await _repository.DeleteEmployeeAsync(employee.Id);
        }
    }
}
