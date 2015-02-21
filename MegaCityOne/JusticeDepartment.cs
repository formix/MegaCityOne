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
        /// <summary>
        /// Returns a dictionary containing Laws to be advised by JudgeDredd.
        /// </summary>
        /// <returns>A Law dictionary.</returns>
        IDictionary<string, Law> GetLaws();
    }
}
