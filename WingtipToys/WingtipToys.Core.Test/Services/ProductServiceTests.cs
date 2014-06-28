// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductServiceTests.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace WingtipToys.Core.Test.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using WingtipToys.Core.Entities;
    using WingtipToys.Core.Services;

    /// <summary>
    ///     Tests for ProductService.
    ///     TODO: Should use IoC to inject services.
    /// </summary>
    public abstract class ProductServiceTests : PersistenceTestsBase
    {
        #region Fields

        /// <summary>
        ///     The product service.
        /// </summary>
        private readonly IProductService productService = new ProductService();

        #endregion

        /// <summary>
        ///     Tests for the GetProducts() method.
        /// </summary>
        [TestFixture]
        public class GetProductsTests : ProductServiceTests
        {
            #region Public Methods and Operators

            /// <summary>
            ///     Tests searching for cars using invalid keywords.
            /// </summary>
            [Test]
            public void TestSearchCarsWithInvalidKeywords()
            {
                IEnumerable<Product> cars = this.productService.GetProducts("Cars", "blah");
                Assert.IsNotNull(cars);
                Assert.IsEmpty(cars, "Should have no search results");
            }

            /// <summary>
            ///     Tests searching for cars using valid description keywords.
            /// </summary>
            [Test]
            public void TestSearchCarsWithValidDescriptionKeywords()
            {
                List<Product> cars = this.productService.GetProducts("Cars", "neutri").ToList();
                Assert.IsNotNull(cars);

                Assert.AreEqual(1, cars.Count, "Convertible Car should be returned");

                Product car = cars.Single();
                Assert.AreEqual(1, car.ProductID, "Convertible Car should be returned");
            }

            /// <summary>
            ///     Tests searching for cars using valid name keywords.
            /// </summary>
            [Test]
            public void TestSearchCarsWithValidNameKeywords()
            {
                List<Product> cars = this.productService.GetProducts("Cars", "conv").ToList();
                Assert.IsNotNull(cars);

                Assert.AreEqual(1, cars.Count, "Convertible Car should be returned");

                Product car = cars.Single();
                Assert.AreEqual(1, car.ProductID, "Convertible Car should be returned");
            }

            /// <summary>
            ///     Tests with the Cars category and no search keywords.
            /// </summary>
            [Test]
            public void TestWithCarsNoSearchKeywords()
            {
                IEnumerable<Product> cars = this.productService.GetProducts("Cars", null);
                Assert.IsNotNull(cars);

                // Test data has 5 cars
                Assert.AreEqual(5, cars.Count(), "No search keywords should return all cars");
            }

            /// <summary>
            ///     Tests with an invalid category.
            /// </summary>
            [Test]
            public void TestWithInvalidCategory()
            {
                IEnumerable<Product> products = this.productService.GetProducts("blah", null);
                Assert.IsNotNull(products);
                Assert.IsEmpty(products, "Invalid category should return 0 products");
            }

            /// <summary>
            ///     Tests with no category and no search keywords.
            /// </summary>
            [Test]
            public void TestWithNoCategoryNoSearchKeywords()
            {
                IEnumerable<Product> products = this.productService.GetProducts(null, null);
                Assert.IsNotNull(products);
                Assert.IsEmpty(products, "No category should return 0 products");
            }

            #endregion
        }
    }
}