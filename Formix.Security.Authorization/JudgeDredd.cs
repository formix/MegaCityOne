using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Formix.Security.Authorization
{
    public class JudgeDredd : AbstractJudge
    {

        #region Methods

        public override bool Advise(string law, params object[] arguments)
        {
            throw new NotImplementedException();
        }

        public override void Enforce(string law, params object[] arguments)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
