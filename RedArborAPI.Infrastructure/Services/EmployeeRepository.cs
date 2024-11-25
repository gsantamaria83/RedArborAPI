using Dapper;
using Microsoft.EntityFrameworkCore;
using RedArborAPI.Domain.Dtos;
using RedArborAPI.Domain.Entities;
using RedArborAPI.Infrastructure.Interfaces;
using RedArborAPI.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedArborAPI.Infrastructure.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbConnection _dbConnection;

        public EmployeeRepository(ApplicationDbContext context, IDbConnection dbConnection)
        {
            _context = context;
            _dbConnection = dbConnection;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return (await _dbConnection.QueryAsync<Employee>("SELECT * FROM Employee")).AsList();
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _context.Employee.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _context.Employee.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employeeIdAsString = id.ToString().Trim();
            var employee = await GetEmployeeByIdAsync(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<Employee>("SELECT * FROM Employee WHERE Id = @Id", new { Id = id });
        }
    }
}
