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

        #region Fields

        private BookOfTheLaw laws;

        #endregion

        #region Properties

        /// <summary>
        /// Laws contained in Dredd's embarked artificial intelligence.
        /// </summary>
        public BookOfTheLaw Laws 
        {
            get { return this.laws; }
        }

        #endregion

        #region Constructors

        public JudgeDredd()
        {
            this.laws = new BookOfTheLaw();
        }

        #endregion

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
            if (newLaws == null)
            {
                throw new ArgumentNullException("newLaws");
            }

            foreach (var law in newLaws)
            {
                this.laws.Add(law.Key, law.Value);
            }
        }

        /// <summary>
        /// Loads a set of laws obtained from the given JusticeDepartment.
        /// </summary>
        /// <param name="justiceDepartment">The JusticeDepartment containing 
        /// the laws to embark in Dredd's AI.</param>
        public void Load(JusticeDepartment justiceDepartment)
        {
            if (justiceDepartment == null)
            {
                throw new ArgumentNullException("justiceDepartment");
            }

            this.Load(justiceDepartment.GetLaws());
        }

        /// <summary>
        /// Seeks all JusticeDeparment from the library and loads the Laws 
        /// they contains in Dredd's AI.
        /// </summary>
        /// <param name="library">The library to search for JusticeDepartment
        /// </param>
        public void Load(Assembly library)
        {
            if (library == null)
            {
                throw new ArgumentNullException("library");
            }

            Type justiceDeptType = typeof(JusticeDepartment);
            foreach (var type in library.ExportedTypes)
            {
                if (justiceDeptType.IsAssignableFrom(type))
                {
                    JusticeDepartment dept = (JusticeDepartment)Activator.CreateInstance(type);
                    this.Load(dept.GetLaws());
                }
            }
        }

        /// <summary>
        /// Search in the given path a library that may contains one or more 
        /// JusticeDepartment. If found, adds the laws obtained to Dredd's AI.
        /// </summary>
        /// <param name="path">The file path to the library containing the 
        /// JusticeDepartment.</param>
        public void Load(string path)
        {
            if (string.IsNullOrEmpty(path.Trim()))
            {
                throw new ArgumentException("The 'path' parameter cannot be null or empty.");
            }

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
            if (string.IsNullOrEmpty(law.Trim()))
            {
                throw new ArgumentException("The 'law' parameter cannot be null or empty.");
            }
            if (!this.laws.ContainsKey(law))
            {
                throw new ArgumentException("The specified law '" + law + "' is not defined.");
            }

            return this.laws[law](this.Principal, arguments);
        }

        #endregion
    }
}
