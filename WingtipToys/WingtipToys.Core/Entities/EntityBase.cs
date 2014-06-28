// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityBase.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WingtipToys.Core.Entities
{
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    /// <summary>
    /// Base class definition for a persisted entity.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of the entity.
    /// </typeparam>
    public abstract class EntityBase<TEntity> : IAutoMappingOverride<TEntity>
        where TEntity : EntityBase<TEntity>, new()
    {
        #region Public Methods and Operators

        /// <summary>
        /// Overrides the specified mapping.
        /// </summary>
        /// <param name="mapping">
        /// The mapping.
        /// </param>
        public virtual void Override(AutoMapping<TEntity> mapping)
        {
            // Default: auto-map all properties
        }

        #endregion
    }
}