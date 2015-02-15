using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MegaCityOne
{
    public abstract class AbstractJudge : Judge
    {

        #region Fields

        private IPrincipal principal;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the principal currently under scrutiny of this Judge.
        /// After the Judge instanciation, the scrutinized principal 
        /// corresponds to the principal found in 
        /// <see cref="System.Threading.Thread.CurrentPrincipal"/>. This is 
        /// the Judge main target. During the course of action, you can bring 
        /// another IPrincipal under scrutiny by setting this value. To take 
        /// the Judge attention back to the main target, sets this property 
        /// back to null.
        /// </summary>
        /// <remarks>Setting a value to this property do not override 
        /// <see cref="System.Threading.Thread.CurrentPrincipal"/>.</remarks>
        public virtual IPrincipal Principal
        {
            get
            {
                if (this.principal == null)
                {
                    return Thread.CurrentPrincipal;
                }
                else
                {
                    return this.principal;
                }
            }

            set
            {
                this.principal = value;
            }
        }

        #endregion

        #region Methods

        public abstract bool Advise(string law, params object[] arguments);

        public virtual void Enforce(string law, params object[] arguments)
        {
            if (!this.Advise(law, arguments))
            {
                string message = "Failed law advice for user: " +
                        this.Principal.Identity.Name +
                        " and law: " + law;
                if (arguments.Length > 0)
                {
                    message += " with the following arguments: " + arguments.ToString();
                }
                throw new LawgiverException(message);
            }
        }

        #endregion
    }
}
