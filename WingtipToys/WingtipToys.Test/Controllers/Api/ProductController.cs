// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductController.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WingtipToys.Test.Controllers.Api
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using WingtipToys.Core.Dtos;
    using WingtipToys.Core.Entities;

    /// <summary>
    ///     Test ProductController.
    /// </summary>
    public class ProductController : ApiController
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <param name="dto">
        /// The product DTO.
        /// </param>
        /// <returns>
        /// The products.
        /// </returns>
        [HttpGet]
        public IEnumerable<Product> GetProducts([FromUri] ProductDto dto)
        {
            // Just return an empty list of products for test purposes.
            return Enumerable.Empty<Product>();
        }

        #endregion
    }
}