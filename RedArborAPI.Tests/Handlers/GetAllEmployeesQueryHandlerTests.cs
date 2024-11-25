using RedArborAPI.Application.Handlers;
using RedArborAPI.Application.Queries;
using RedArborAPI.Domain.Entities;
using RedArborAPI.Tests.FakeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArborAPI.Tests.Handlers
{
    public class GetAllEmployeesQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnAllEmployees()
        {
            // Arrange
            var fakeRepository = new FakeEmployeeRepository();
            var handler = new GetAllEmployeesQueryHandler(fakeRepository);

            var employees = new List<Employee>
        {
            new Employee { Id = 1, Username = "User1" },
            new Employee { Id = 2, Username = "User2" }
        };

            foreach (var employee in employees)
            {
                await fakeRepository.AddEmployeeAsync(employee);
            }

            var query = new GetAllEmployeesQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, e => e.Username == "User1");
            Assert.Contains(result, e => e.Username == "User2");
        }

        [Fact]
        public async Task Handle_WhenNoEmployees_ShouldReturnEmptyList()
        {
            // Arrange
            var fakeRepository = new FakeEmployeeRepository();
            var handler = new GetAllEmployeesQueryHandler(fakeRepository);

            var query = new GetAllEmployeesQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
