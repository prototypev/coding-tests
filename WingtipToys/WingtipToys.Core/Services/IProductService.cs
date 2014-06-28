// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProductService.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WingtipToys.Core.Services
{
    using System.Collections.Generic;

    using WingtipToys.Core.Entities;

    /// <summary>
    ///     Definition for services for Product related persistence functions.
    /// </summary>
    public interface IProductService
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <param name="categoryName">
        /// Name of the category.
        /// </param>
        /// <param name="searchKeywords">
        /// The search keywords.
        /// </param>
        /// <returns>
        /// The products.
        /// </returns>
        IEnumerable<Product> GetProducts(string categoryName, string searchKeywords);

        #endregion
    }
}