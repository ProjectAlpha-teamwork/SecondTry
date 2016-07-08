using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users;

namespace BetSystem.Interfaces
{
    interface ILogable
    {
        User SignIn(string userName, string password);
    }
}
