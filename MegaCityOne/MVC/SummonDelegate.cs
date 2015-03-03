using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCityOne.MVC
{
    /// <summary>
    /// This delegate is used to define a JudegSummon event.
    /// </summary>
    /// <param name="source">The source of the event.</param>
    /// <param name="e">The Judge summon event args.</param>
    public delegate void SummonDelegate(object source, SummonEventArgs e);
}
