using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCityOne.Tests.Judges
{

    public class JudgeDummy : AbstractJudge
    {
        public delegate bool Advisable(string law, params object[] arguments);

        private Advisable adviseProxy;

        public override ICollection<string> Rules
        {
            get { return new List<string>(); }
        }

        /// <summary>
        /// Creates a dummy Judge that always Advise to true!
        /// </summary>
        public JudgeDummy() : this((l, a) => true) { }

        /// <summary>
        /// Creates a dummy judge that recieve an advisable function pointer to allow easy testing.
        /// </summary>
        /// <param name="proxy"></param>
        public JudgeDummy(Advisable adviseProxy)
        {
            this.adviseProxy = adviseProxy;
        }

        public override bool Advise(string law, params object[] arguments)
        {
            return this.adviseProxy(law, arguments);
        }
    }
}
