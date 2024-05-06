using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results;

public sealed class AccessingErrorResultValueException : Exception
{
    public AccessingErrorResultValueException(string message)
        : base(message) { }
}
