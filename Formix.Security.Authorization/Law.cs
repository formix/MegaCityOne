using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Formix.Security.Authorization
{
    public delegate bool Law(IPrincipal principal, params object[] arguments);
}
