using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MegaCityOne
{
    public class JudgeDredd : AbstractJudge
    {
        private BookOfTheLaw laws;


        /// <summary>
        /// Laws contained in Dredd's artificial AI.
        /// </summary>
        public BookOfTheLaw Laws 
        {
            get { return this.laws; }
        }

        public JudgeDredd()
        {
            this.laws = new BookOfTheLaw();
        }

        #region Methods

        /// <summary>
        /// Loads a new set of Laws in Dredd's helmet computer. The New Laws 
        /// do not invalidate existing ones systematically. Instead, they are
        /// added to Dredd's embarked artificial intelligence. Note that a new 
        /// Law having the same name as a pre-existing Law will override it.
        /// </summary>
        /// <param name="newLaws">A set of new laws to add to Dredd's embarked 
        /// AI</param>
        public void Load(BookOfTheLaw newLaws)
        {
            foreach (var law in newLaws)
            {
                this.laws.Add(law.Key, law.Value);
            }
        }

        /// <summary>
        /// Loads a set of laws obtained from the given IJusticeDepartment.
        /// </summary>
        /// <param name="justiceDepartment">The IJusticeDepartment containing 
        /// the laws to embark in Dredd's AI.</param>
        public void Load(IJusticeDepartment justiceDepartment)
        {
            this.Load(justiceDepartment.GetLaws());
        }

        /// <summary>
        /// Seeks all IJusticeDeparment from the library and loads the Laws 
        /// they contains in Dredd's AI.
        /// </summary>
        /// <param name="library">The library to search for IJusticeDepartment
        /// </param>
        public void Load(Assembly library)
        {
            Type justiceDeptType = typeof(IJusticeDepartment);
            foreach (var type in library.ExportedTypes)
            {
                if (justiceDeptType.IsAssignableFrom(type))
                {
                    IJusticeDepartment dept = (IJusticeDepartment)Activator.CreateInstance(type);
                    this.Load(dept.GetLaws());
                }
            }
        }

        /// <summary>
        /// Search in the given path a library that may contains one or more 
        /// IJusticeDepartment. If found, adds the laws obtained to Dredd's AI.
        /// </summary>
        /// <param name="path">The file path to the library containing the 
        /// IJusticeDepartment.</param>
        public void Load(string path)
        {
            Assembly assembly = Assembly.LoadFrom(path);
            this.Load(assembly);
        }

        /// <summary>
        /// Dredd checks with it's AI and tells if the given law is repsected 
        /// or not, given the provided arguments, for the current Principal.
        /// </summary>
        /// <param name="law">The Law to be advised</param>
        /// <param name="arguments">Optional arguments to be evaluated.</param>
        /// <returns></returns>
        public override bool Advise(string law, params object[] arguments)
        {
            return this.laws[law](this.Principal, arguments);
        }

        #endregion
    }
}
