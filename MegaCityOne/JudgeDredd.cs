using System;
using System.Collections.Generic;
using System.Reflection;

namespace MegaCityOne
{
    /// <summary>
    /// JudgeDredd is a Judge that uses Laws defined as lambda expressions.
    /// </summary>
    public class JudgeDredd : AbstractJudge
    {

        #region Fields

        private IDictionary<string, Law> laws;

        #endregion

        #region Properties

        /// <summary>
        /// Laws contained in Dredd's embarked artificial intelligence.
        /// </summary>
        public virtual IDictionary<string, Law> Laws 
        {
            get { return this.laws; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Instanciates JudgeDredd.
        /// </summary>
        public JudgeDredd()
        {
            this.laws = new Dictionary<string, Law>();
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
        public virtual void Load(IDictionary<string, Law> newLaws)
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
        public virtual void Load(JusticeDepartment justiceDepartment)
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
        public virtual void Load(Assembly library)
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
        public virtual void Load(string path)
        {
            if (string.IsNullOrEmpty(path.Trim()))
            {
                throw new ArgumentException("The 'path' parameter cannot be null or empty.");
            }

            Assembly assembly = Assembly.LoadFrom(path);
            this.Load(assembly);
        }

        /// <summary>
        /// Search for a JusticeDepartment implementation in the calling 
        /// library.
        /// </summary>
        public void Load()
        {
            this.Load(Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// Dredd checks with it's AI and tells if the given law is repsected 
        /// or not, given the provided arguments, for the current Principal.
        /// </summary>
        /// <param name="law">The law to be advised.</param>
        /// <param name="arguments">Any system state that could help the 
        /// Judge to give a relevant advice regarding the law in question.</param>
        /// <returns>True if the advised law is respected for the current Principal, 
        /// given optional arguments. False otherwise.</returns>
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

            if (!this.Principal.Identity.IsAuthenticated)
            {
                return false;
            }

            return this.laws[law](this.Principal, arguments);
        }

        #endregion
    }
}
