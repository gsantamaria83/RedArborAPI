using Microsoft.EntityFrameworkCore;
using RedArborAPI.Domain.Entities;
using RedArborAPI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArborAPI.Tests.FakeRepository
{
    internal class FakeEmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employees = new();

        public Task AddEmployeeAsync(Employee employee)
        {
            employee.Id = _employees.Count + 1; // Simula la generación de un ID
            _employees.Add(employee);
            return Task.CompletedTask;
        }

        public Task UpdateEmployeeAsync(Employee employee)
        {
            var existingEmployee = _employees.FirstOrDefault(e => e.Id == employee.Id);
            if (existingEmployee == null)
                return Task.FromResult(false);

            existingEmployee.Username = employee.Username;
            existingEmployee.Email = employee.Email;
            existingEmployee.UpdatedOn = employee.UpdatedOn;

            return Task.FromResult(true);
        }

        public Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return Task.FromResult(_employees.FirstOrDefault(e => e.Id == id));
        }

        public Task<List<Employee>> GetAllEmployeesAsync()
        {
            return Task.FromResult(_employees.ToList());
        }

        public Task DeleteEmployeeAsync(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _employees.Remove(employee);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
