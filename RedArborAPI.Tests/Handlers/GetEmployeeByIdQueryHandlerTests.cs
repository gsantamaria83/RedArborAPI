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
    public class GetEmployeeByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_GivenExistingEmployee_ShouldReturnEmployee()
        {
            // Arrange
            var fakeRepository = new FakeEmployeeRepository();
            var handler = new GetEmployeeByIdQueryHandler(fakeRepository);

            var employee = new Employee { Id = 1, Username = "TestUser" };
            await fakeRepository.AddEmployeeAsync(employee);

            var query = new GetEmployeeByIdQuery { Id = 1 };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("TestUser", result?.Username);
        }
    }
}
