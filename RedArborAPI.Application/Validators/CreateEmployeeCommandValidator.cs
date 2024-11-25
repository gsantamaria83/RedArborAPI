﻿using FluentValidation;
using RedArborAPI.Application.Commands;
using RedArborAPI.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArborAPI.Application.Validators
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator() 
        {
            RuleFor(x => x.Email).ValidEmail();
            RuleFor(x => x.CompanyId).NotNullOrZero();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(4).WithMessage("Password must have at least 6 characters.");
            RuleFor(x => x.PortalId).NotNullOrZero();
            RuleFor(x => x.RoleId).NotNullOrZero();
            RuleFor(x => x.StatusId).NotEmpty().WithMessage("Status must not be empty");
            RuleFor(x => x.Username).NotEmpty().MinimumLength(4).WithMessage("User name must have at least 8 characters.");

        }

    }
}
