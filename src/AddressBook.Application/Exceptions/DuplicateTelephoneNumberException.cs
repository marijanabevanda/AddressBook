using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Application.Exceptions
{
    public class DuplicateTelephoneNumberException : Exception
    {
        public DuplicateTelephoneNumberException(string message) : base(message)
        {
            
        }
    }
}
