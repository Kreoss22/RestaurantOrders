using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message) : base(message) { }
    }

    public class InvalidPeselException : Exception
    {
        public InvalidPeselException(string message) : base(message) { }
    }

    public class DuplicateEmailException : Exception
    {
        public DuplicateEmailException(string message) : base(message) { }
    }
}

