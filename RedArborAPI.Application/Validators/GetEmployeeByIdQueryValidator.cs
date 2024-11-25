using FluentValidation;
using RedArborAPI.Application.Queries;
using RedArborAPI.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArborAPI.Application.Validators
{
    public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
    {
        public GetEmployeeByIdQueryValidator() 
        {
            RuleFor(x => x.Id).NotNullOrZero();
        }
    }
}
