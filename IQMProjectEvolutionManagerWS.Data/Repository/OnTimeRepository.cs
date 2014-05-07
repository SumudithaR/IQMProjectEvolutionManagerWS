// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OnTimeRepository.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   The on time repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;

    using IQMProjectEvolutionManagerWS.Data.Interfaces.Repository;

    /// <summary>
    /// The on time repository.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1622:GenericTypeParameterDocumentationMustHaveText", Justification = "Reviewed. Suppression is OK here.")]
    public class OnTimeRepository<T> : IOnTimeRepository<T>
        where T : class
    {
        /// <summary>
        /// The ontime db context.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private readonly OnTime10Entities ontimeDBContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTimeRepository{T}"/> class.
        /// </summary>
        public OnTimeRepository()
        {
            this.ontimeDBContext = new OnTime10Entities();
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<T> GetAll()
        {
            return this.ontimeDBContext.Set<T>().ToList();
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<T> GetAll(Expression<Func<T, bool>> filter)
        {
            return this.ontimeDBContext.Set<T>().Where(filter).ToList();
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T GetById(object id)
        {
            return this.ontimeDBContext.Set<T>().Find(id);
        }
    }
}