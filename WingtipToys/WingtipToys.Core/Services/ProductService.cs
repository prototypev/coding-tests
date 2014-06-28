// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductService.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace WingtipToys.Core.Services
{
    using System.Collections.Generic;

    using NHibernate.Criterion;

    using WingtipToys.Core.Entities;
    using WingtipToys.Core.Infrastructure.Persistence;

    /// <summary>
    ///     Services for Product related persistence functions.
    /// </summary>
    public class ProductService : IProductService
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <returns>
        /// The products.
        /// </returns>
        public virtual IEnumerable<Product> GetProducts(string categoryName, string searchKeywords)
        {
            var query = SessionManager.Instance.CurrentSession.QueryOver<Product>();

            if (!string.IsNullOrWhiteSpace(searchKeywords))
            {
                string trimmedSearchKeywords = searchKeywords.Trim();

                query.Where(Restrictions.Disjunction()
                    .Add(Restrictions.On<Product>(product => product.ProductName).IsInsensitiveLike(trimmedSearchKeywords, MatchMode.Anywhere))
                    .Add(Restrictions.On<Product>(product => product.Description).IsInsensitiveLike(trimmedSearchKeywords, MatchMode.Anywhere)));
            }

            string trimmedCategoryName = categoryName == null ? null : categoryName.Trim();
            return query.JoinQueryOver(product => product.Category)
                .Where(category => category.CategoryName == trimmedCategoryName)
                .List();
        }

        #endregion
    }
}