using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCityOne.Mvc
{
    /// <summary>
    /// This delegate is used to define a Summon event.
    /// </summary>
    /// <param name="source">The source of the event.</param>
    /// <param name="e">The Judge summon event args.</param>
    public delegate void SummonDelegate(object source, SummonEventArgs e);
}
