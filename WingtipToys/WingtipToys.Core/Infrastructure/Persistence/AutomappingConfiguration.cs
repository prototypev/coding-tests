// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutomappingConfiguration.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WingtipToys.Core.Infrastructure.Persistence
{
    using System;

    using FluentNHibernate.Automapping;

    using WingtipToys.Core.Entities;
    using WingtipToys.Core.Infrastructure.Utilities;

    /// <summary>
    ///     Controls the behaviour of the Fluent NHibernate auto mapper.
    /// </summary>
    public class AutomappingConfiguration : DefaultAutomappingConfiguration
    {
        #region Public Methods and Operators

        /// <summary>
        /// Determines whether the Fluent NHibernate automapper should map this type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// True if we should map.
        /// </returns>
        public override bool ShouldMap(Type type)
        {
            return !type.IsAbstract && type.IsSubclassOfRawGeneric(typeof(EntityBase<>));
        }

        #endregion
    }
}