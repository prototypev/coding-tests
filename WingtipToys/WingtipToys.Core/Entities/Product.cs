// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace WingtipToys.Core.Entities
{
    using System.ComponentModel.DataAnnotations;

    using FluentNHibernate.Automapping;

    using WingtipToys.Core.Infrastructure.Utilities;

    /// <summary>
    ///     Definition for a product.
    /// </summary>
    public class Product : EntityBase<Product>
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the category.
        /// </summary>
        /// <value>
        ///     The category.
        /// </value>
        [Required]
        public virtual Category Category { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        [DataType(DataType.MultilineText)]
        public virtual string Description { get; set; }

        /// <summary>
        ///     Gets or sets the image path.
        /// </summary>
        /// <value>
        ///     The image path.
        /// </value>
        [DataType(DataType.ImageUrl)]
        public virtual string ImagePath { get; set; }

        /// <summary>
        ///     Gets or sets the product identifier.
        /// </summary>
        /// <value>
        ///     The product identifier.
        /// </value>
        public virtual int ProductID { get; set; }

        /// <summary>
        ///     Gets or sets the name of the product.
        /// </summary>
        /// <value>
        ///     The name of the product.
        /// </value>
        [Required]
        public virtual string ProductName { get; set; }

        /// <summary>
        ///     Gets or sets the unit price.
        /// </summary>
        /// <value>
        ///     The unit price.
        /// </value>
        [DataType(DataType.Currency)]
        public virtual float UnitPrice { get; set; }

        #endregion

        /// <summary>
        /// Overrides the specified mapping.
        /// </summary>
        /// <param name="mapping">The mapping.</param>
        public override void Override(AutoMapping<Product> mapping)
        {
            mapping.Table("Products");

            // Not creating any new Products for this exercise, so don't need to bother about identity generation
            mapping.Id(product => product.ProductID).GeneratedBy.Assigned();

            mapping.References(product => product.Category)
                .Column(ReflectionHelper.MembersOf<Category>.GetName(category => category.CategoryID))
                .Not.Nullable()
                .Fetch.Join();
        }
    }
}