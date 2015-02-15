using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MegaCityOne
{
    /// <summary>
    /// JudgeRico works differently from JudgeDredd and JudgeAnderson. He 
    /// judges based on a claim predicate or claim type and a claim value. 
    /// That's why he expects to deal with ClaimPrincipal instances.
    /// </summary>
    public class JudgeRico : AbstractJudge
    {
        /// <summary>
        /// JudgeRico advise based on a claim type and a claim value for 
        /// a <see cref="System.Security.Claims.ClaimsPrincipal"/>. Thus 
        /// he will not regard the given law and handle the situation depending 
        /// on the Principal's claim predicate or the claim type and claim value.
        /// </summary>
        /// <param name="law">Not used by JudgeRico</param>
        /// <param name="arguments">Either one or two.
        /// 
        /// One argument: 
        ///     Argument 0: Must be a <see cref="System.Predicate<Claim>"/>.
        /// 
        /// Two arguments:
        ///     Argument 0: A string denoting the claim type
        ///     Argument 1: A string denoting the claim value
        ///     
        /// </param>
        /// <seealso cref="System.Security.Claims.Claim"/>
        /// <returns>True if the claim information match one of the current 
        /// ClaimPrincipal claims type and values, false otherwise.</returns>
        public override bool Advise(string law, params object[] arguments)
        {
            if (!(this.Principal is ClaimsPrincipal))
            {
                throw new InvalidOperationException("JudgeRico expects to deal with an instance of ClaimsPrincipal. Current instance type is: " + this.Principal.GetType().FullName);
            }

            var principal = (ClaimsPrincipal)this.Principal;

            if (arguments.Length == 1)
            {
                if (!(arguments[0] is Predicate<Claim>))
                {
                    throw new ArgumentException("When arguments.Length == 1, the argument[0] is expected to be a Predicate<Claim>.");
                }
                var predicate = (Predicate<Claim>) arguments[0];
                return principal.HasClaim(predicate);
            }


            if ((arguments.Length != 2) || !(arguments[0] is string) || !(arguments[1] is string))
            {
                throw new ArgumentException("When arguments.Length == 2, JudgeRico expects to receive thow arguments of type string. The claim type and the claim value.");
            }

            return principal.HasClaim((string)arguments[0], (string)arguments[1]);
        }
    }
}
