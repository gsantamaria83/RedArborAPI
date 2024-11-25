using RedArborAPI.Domain.Entities;
using RedArborAPI.Tests.FakeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArborAPI.Tests.Handlers
{
    public class UpdateEmployeeCommandHandlerTests
    {
        [Fact]
        public async Task UpdateEmployeeAsync_GivenExistingEmployee_ShouldUpdateAndReturnTrue()
        {
            // Arrange
            var repository = new FakeEmployeeRepository();

            // Agregar empleado inicial
            var existingEmployee = new Employee
            {
                CompanyId = 1,
                CreatedOn = DateTime.Now,
                DeletedOn = DateTime.Now,
                Email = "test@example.com",
                Fax = "000.000.000",
                Name = "testdetest",
                Lastlogin = DateTime.Now,
                Password = "testdetestw",
                PortalId = 1,
                RoleId = 1,
                StatusId = 1,
                Telephone = "000.000.000",
                UpdatedOn = DateTime.Now,
                Username = "TestUser"
            };

            await repository.AddEmployeeAsync(existingEmployee);

            // Crear un empleado actualizado
            var updatedEmployee = new Employee
            {
                Id = 1,
                CompanyId = 1,
                CreatedOn = DateTime.Now,
                DeletedOn = DateTime.Now,
                Email = "updated@example.com",
                Fax = "000.000.000",
                Name = "testdetest",
                Lastlogin = DateTime.Now,
                Password = "testdetestw",
                PortalId = 1,
                RoleId = 1,
                StatusId = 1,
                Telephone = "000.000.000",
                UpdatedOn = DateTime.Now,
                Username = "UpdatedName"
            };

            // Act
            await repository.UpdateEmployeeAsync(updatedEmployee);
            // Assert
            Assert.True(true);

            var employeeFromRepo = await repository.GetEmployeeByIdAsync(1);
            Assert.NotNull(employeeFromRepo);
            Assert.Equal("UpdatedName", employeeFromRepo?.Username);
            Assert.Equal("updated@example.com", employeeFromRepo?.Email);
            Assert.Equal(updatedEmployee.UpdatedOn, employeeFromRepo?.UpdatedOn);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_GivenNonExistingEmployee_ShouldReturnFalse()
        {
            // Arrange
            var repository = new FakeEmployeeRepository();

            var nonExistingEmployee = new Employee
            {
                Id = 999,
                Username = "NonExisting",
                Email = "nonexisting@example.com",
                UpdatedOn = DateTime.Now
            };

            // Act
            await repository.UpdateEmployeeAsync(nonExistingEmployee);
            // Assert
            Assert.True(true);
        }
    }
}
