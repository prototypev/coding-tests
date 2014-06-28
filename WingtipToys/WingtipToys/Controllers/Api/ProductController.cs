// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductController.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace WingtipToys.Controllers.Api
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Web.Http;

    using WingtipToys.Core.Dtos;
    using WingtipToys.Core.Entities;
    using WingtipToys.Core.Services;

    /// <summary>
    ///     Controller for Product related functions.
    /// </summary>
    public class ProductController : WebApiControllerBase
    {
        #region Fields

        /// <summary>
        ///     The product service.
        /// </summary>
        private readonly IProductService productService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProductController" /> class.
        ///     TODO: Should use IoC to inject services. Without it, we can't test the controller-actions cleanly.
        /// </summary>
        public ProductController()
        {
            this.productService = new ProductService();
        }

        #endregion

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
            Contract.Requires(dto != null, "ASP.NET web api should resolve the DTO and create it always.");

            return this.productService.GetProducts(dto.CategoryName, dto.SearchKeywords);
        }

        #endregion
    }
}