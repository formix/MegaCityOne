using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MegaCityOne
{
    public delegate bool Law(IPrincipal principal, object[] arguments);
}
