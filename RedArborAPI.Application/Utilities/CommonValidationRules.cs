using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArborAPI.Application.Utilities
{
    public static class CommonValidationRules
    {
        public static IRuleBuilderOptions<T, string> ValidEmail<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotEmpty().EmailAddress().WithMessage("A valid email is required.");
        }
        public static IRuleBuilderOptions<T, int> NotNullOrZero<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder
                .GreaterThan(0) 
                .WithMessage("{PropertyName} must not be null or zero.");
        }
    }
    
}
