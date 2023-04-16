using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Application.Exceptions
{
    public class DuplicateContactException : Exception
    {
        public DuplicateContactException(string message) : base(message) { }
    }
}
