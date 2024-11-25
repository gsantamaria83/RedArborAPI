using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedArborAPI.Application.Commands;
using RedArborAPI.Application.Queries;
using RedArborAPI.Domain.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RedArborAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RedArborController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RedArborController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _mediator.Send(new GetAllEmployeesQuery());
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery { Id = id });
            if (employee == null) {
                return Ok(new ResponseDto<bool?>(false,"Employee not found"));
            } 
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command, [FromServices] IValidator<CreateEmployeeCommand> validator)
        {
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var employeeId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeId }, command);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeCommand command, [FromServices] IValidator<UpdateEmployeeCommand> validator)
        {
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (id != command.Id) return BadRequest();
            await _mediator.Send(command);
            return Ok(new ResponseDto<bool?>(true, "Employee was updated sucessfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id,[FromServices] IValidator<DeleteEmployeeCommand> validator)
        {
            var deleteCommand = new DeleteEmployeeCommand { Id = id };

            var validationResult = await validator.ValidateAsync(deleteCommand);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _mediator.Send(deleteCommand);
            return Ok(new ResponseDto<bool?>(true, "Employee was deleted sucessfully"));
        }
    }
}
