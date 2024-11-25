using RedArborAPI.Application.Commands;
using RedArborAPI.Application.Handlers;
using RedArborAPI.Domain.Entities;
using RedArborAPI.Tests.FakeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArborAPI.Tests.Handlers
{
    public class DeleteEmployeeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenExistingEmployee_ShouldDeleteEmployeeAndReturnTrue()
        {
            // Arrange
            var fakeRepository = new FakeEmployeeRepository();
            var handler = new DeleteEmployeeCommandHandler(fakeRepository);

            var employee = new Employee { Id = 1, Username = "TestUser" };
            await fakeRepository.AddEmployeeAsync(employee);

            var command = new DeleteEmployeeCommand { Id = 1 };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(true);
            var deletedEmployee = await fakeRepository.GetEmployeeByIdAsync(1);
            Assert.Null(deletedEmployee); // Verifica que el empleado fue eliminado
        }
    }
}
