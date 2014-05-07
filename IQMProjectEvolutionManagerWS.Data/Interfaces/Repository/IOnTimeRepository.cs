// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOnTimeRepository.cs" company="IQM Software">
//   Sumuditha Ranawaka 2014.
// </copyright>
// <summary>
//   Defines the IOnTimeRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IQMProjectEvolutionManagerWS.Data.Interfaces.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;

    /// <summary>
    /// The OnTimeRepository interface.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1622:GenericTypeParameterDocumentationMustHaveText", Justification = "Reviewed. Suppression is OK here.")]
    public interface IOnTimeRepository<T>
    {
        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        IList<T> GetAll();

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        IList<T> GetAll(Expression<Func<T, bool>> filter);

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T GetById(object id);
    }
}
