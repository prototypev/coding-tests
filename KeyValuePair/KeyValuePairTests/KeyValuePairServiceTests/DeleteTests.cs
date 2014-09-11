namespace KeyValuePairTests.KeyValuePairServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using KeyValuePair;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the Delete() method.
    /// </summary>
    [TestClass]
    public class DeleteTests : KeyValuePairServiceTestsBase
    {
        #region Public Methods and Operators

        /// <summary>
        /// Tests with null items.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestWithNullItems()
        {
            this.KeyValuePairService.Delete(null);
        }

        /// <summary>
        /// Tests with valid items.
        /// </summary>
        [TestMethod]
        public void TestWithValidItems()
        {
            this.KeyValuePairService.CreateOrUpdate("a=d");
            this.KeyValuePairService.CreateOrUpdate("c=b");
            this.KeyValuePairService.CreateOrUpdate("b=c");
            this.KeyValuePairService.CreateOrUpdate("d=a");

            this.KeyValuePairService.Delete(new[] { "a=d", "b=c" });
            KeyValuePair<string, string>[] pairs = this.KeyValuePairService.GetAll(SortOrder.NAME)
                                                       .ToArray();
            Assert.AreEqual(2, pairs.Length);

            Assert.AreEqual("c", pairs[0].Key);
            Assert.AreEqual("b", pairs[0].Value);

            Assert.AreEqual("d", pairs[1].Key);
            Assert.AreEqual("a", pairs[1].Value);
        }

        #endregion
    }
}