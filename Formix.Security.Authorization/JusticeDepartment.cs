using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCityOne
{
    /// <summary>
    /// A JusticeDepartment instance must have a default constructor.
    /// </summary>
    public interface JusticeDepartment
    {
        BookOfTheLaw GetLaws();
    }
}
