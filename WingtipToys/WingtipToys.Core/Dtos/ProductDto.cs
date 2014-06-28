// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductDto.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WingtipToys.Core.Dtos
{
    /// <summary>
    ///     A data transfer object for Product.
    /// </summary>
    public class ProductDto
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the name of the category.
        /// </summary>
        /// <value>
        ///     The name of the category.
        /// </value>
        public string CategoryName { get; set; }

        /// <summary>
        ///     Gets or sets the search keywords.
        /// </summary>
        /// <value>
        ///     The search keywords.
        /// </value>
        public string SearchKeywords { get; set; }

        #endregion
    }
}