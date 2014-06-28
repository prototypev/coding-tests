// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Category.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace WingtipToys.Core.Entities
{
    using System.ComponentModel.DataAnnotations;

    using FluentNHibernate.Automapping;

    /// <summary>
    ///     Definition for a category
    /// </summary>
    public class Category : EntityBase<Category>
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the category identifier.
        /// </summary>
        /// <value>
        ///     The category identifier.
        /// </value>
        public virtual int CategoryID { get; set; }

        /// <summary>
        ///     Gets or sets the name of the category.
        /// </summary>
        /// <value>
        ///     The name of the category.
        /// </value>
        [Required]
        public virtual string CategoryName { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        [DataType(DataType.MultilineText)]
        public virtual string Description { get; set; }

        #endregion

        /// <summary>
        /// Overrides the specified mapping.
        /// </summary>
        /// <param name="mapping">The mapping.</param>
        public override void Override(AutoMapping<Category> mapping)
        {
            mapping.Table("Categories");

            // Not creating any new Categories for this exercise, so don't need to bother about identity generation
            mapping.Id(category => category.CategoryID).GeneratedBy.Assigned();
        }
    }
}