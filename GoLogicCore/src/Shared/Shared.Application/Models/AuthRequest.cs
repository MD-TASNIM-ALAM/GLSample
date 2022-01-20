using System;
using MediatR;
using System.Collections.Generic;
using System.Text;
using Shared.Domain.Models;

namespace Shared.Application.Models
{
    public abstract class AuthRequest<T> : IRequest<Result<T>>
    {
        public User User { get; set; }
    }
}
