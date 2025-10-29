using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Primitives
{
    public sealed record Error(string Code, string? Message = null)
    {
        public static Error None => new Error(string.Empty);
    }
}
