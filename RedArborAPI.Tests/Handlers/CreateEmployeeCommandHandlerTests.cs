using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using RedArborAPI.Application.Commands;
using RedArborAPI.Application.Handlers;
using RedArborAPI.Domain.Entities;
using RedArborAPI.Infrastructure.Persistence;
using RedArborAPI.Tests.FakeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArborAPI.Tests.Handlers
{
    public class CreateEmployeeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidRequest_ShouldCreateEmployeeAndReturnId()
        {
            var fakeRepository = new FakeEmployeeRepository();
            var handler = new CreateEmployeeCommandHandler(fakeRepository);

            var command = new CreateEmployeeCommand
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

            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(1, result); // Verifica que el ID retornado es 1

            var employee = await fakeRepository.GetEmployeeByIdAsync(1);
            Assert.NotNull(employee);
            Assert.Equal("TestUser", employee?.Username);
            Assert.Equal("test@example.com", employee?.Email);
            Assert.Equal(command.CreatedOn, employee?.CreatedOn);
        }

        [Fact]
        public async Task Handle_GivenMultipleRequests_ShouldAssignUniqueIds()
        {
            // Arrange
            var fakeRepository = new FakeEmployeeRepository();
            var handler = new CreateEmployeeCommandHandler(fakeRepository);

            var command1 = new CreateEmployeeCommand
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
                Username = "TestUser1"
            };

            var command2 = new CreateEmployeeCommand
            {
                CompanyId = 2,
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
                Username = "TestUser2"
            };

            // Act
            var result1 = await handler.Handle(command1, CancellationToken.None);
            var result2 = await handler.Handle(command2, CancellationToken.None);

            // Assert
            Assert.Equal(1, result1); // El primer empleado tiene ID 1
            Assert.Equal(2, result2); // El segundo empleado tiene ID 2

            var employee1 = await fakeRepository.GetEmployeeByIdAsync(1);
            var employee2 = await fakeRepository.GetEmployeeByIdAsync(2);

            Assert.NotNull(employee1);
            Assert.NotNull(employee2);
            Assert.Equal("TestUser1", employee1?.Username);
            Assert.Equal("TestUser2", employee2?.Username);
        }
    }
}
