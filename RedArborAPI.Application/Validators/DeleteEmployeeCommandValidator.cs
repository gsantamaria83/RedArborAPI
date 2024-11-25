using FluentValidation;
using RedArborAPI.Application.Commands;
using RedArborAPI.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArborAPI.Application.Validators
{
    public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeCommandValidator() 
        {
            RuleFor(x => x.Id).NotNullOrZero();
        } 
    }
}
