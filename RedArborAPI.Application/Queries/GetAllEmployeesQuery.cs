﻿using MediatR;
using RedArborAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArborAPI.Application.Queries
{
    public class GetAllEmployeesQuery : IRequest<List<Employee>>
    {
    }
}