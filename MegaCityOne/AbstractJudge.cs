using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MegaCityOne
{
    /// <summary>
    /// Basic implementation of a Judge.
    /// </summary>
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
        /// System.Threading.Thread.CurrentPrincipal. This is 
        /// the Judge main target. During the course of action, you can bring 
        /// another IPrincipal under scrutiny by setting this value. To take 
        /// the Judge attention back to the main target, sets this property 
        /// back to null.</summary>
        /// <remarks>Setting a value to this property do not override 
        /// System.Threading.Thread.CurrentPrincipal.</remarks>
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


        /// <summary>
        /// Returns all rule names useable by the current Judge.
        /// </summary>
        public abstract ICollection<string> Rules { get; }
        
        #endregion

        #region Methods

        /// <summary>
        /// The advise method to be implemented by the specialized Judges.
        /// </summary>
        /// <param name="law">The name of a Law to be interpreted by the 
        /// Judge.</param>
        /// <param name="arguments">Optional arguments given to the Judge to 
        /// interpret a Law properly.</param>
        /// <returns>True if the Law is respected, False otherwise.</returns>
        public abstract bool Advise(string law, params object[] arguments);

        /// <summary>
        /// Standard Law enforcement inplementation. This implementation will 
        /// advise the given Law with the provided optional arguments. If the
        /// Law advises falsely, throws a LawgiverException. If it advises 
        /// truthy then this method does nothing.
        /// </summary>
        /// <param name="law">The name of a Law to be interpreted by the 
        /// Judge.</param>
        /// <param name="arguments">Optional arguments given to the Judge to 
        /// interpret a Law properly.</param>
        /// <exception cref="MegaCityOne.LawgiverException">Thrown when the 
        /// given Law advise falsely with the optional arguments.</exception>
        public void Enforce(string law, params object[] arguments)
        {
            if (!this.Advise(law, arguments))
            {
                string message = "Failed law advisal for principal: " +
                        this.Principal.Identity.Name +
                        " (" + law + "). Sentence: Death by Exception.";
                throw new LawgiverException(message);
            }
        }

        #endregion
    }
}
