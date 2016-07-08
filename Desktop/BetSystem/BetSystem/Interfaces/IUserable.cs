using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users;

namespace BetSystem.Interfaces
{
    interface IUserable
    {
        string UserName { get; }
        string FirstName { get; }
        string LastName { get; }
        string Ssn { get; }
        decimal Balance { get; }
        Gender Gender { get; }
        string PassWord { get; }
        string BackUpCode { get; }
        string Address { get; }
        DateTime BirthDay { get; }
    }
}
