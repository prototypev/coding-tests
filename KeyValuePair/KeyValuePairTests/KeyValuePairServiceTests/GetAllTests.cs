namespace KeyValuePairTests.KeyValuePairServiceTests
{
    using System.Collections.Generic;
    using System.Linq;

    using KeyValuePair;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests for GetAll() method.
    /// </summary>
    [TestClass]
    public class GetAllTests : KeyValuePairServiceTestsBase
    {
        #region Public Methods and Operators

        /// <summary>
        /// Tests sorting by name.
        /// </summary>
        [TestCategory("GetAll")]
        [TestMethod]
        public void TestSortByName()
        {
            this.KeyValuePairService.CreateOrUpdate("a=d");
            this.KeyValuePairService.CreateOrUpdate("c=b");
            this.KeyValuePairService.CreateOrUpdate("b=c");
            this.KeyValuePairService.CreateOrUpdate("d=a");

            KeyValuePair<string, string>[] pairs = this.KeyValuePairService.GetAll(SortOrder.NAME)
                                                       .ToArray();
            Assert.AreEqual(4, pairs.Length);

            Assert.AreEqual("a", pairs[0].Key);
            Assert.AreEqual("d", pairs[0].Value);

            Assert.AreEqual("b", pairs[1].Key);
            Assert.AreEqual("c", pairs[1].Value);

            Assert.AreEqual("c", pairs[2].Key);
            Assert.AreEqual("b", pairs[2].Value);

            Assert.AreEqual("d", pairs[3].Key);
            Assert.AreEqual("a", pairs[3].Value);
        }

        /// <summary>
        /// Tests sorting by value.
        /// </summary>
        [TestCategory("GetAll")]
        [TestMethod]
        public void TestSortByValue()
        {
            this.KeyValuePairService.CreateOrUpdate("a=d");
            this.KeyValuePairService.CreateOrUpdate("c=b");
            this.KeyValuePairService.CreateOrUpdate("b=c");
            this.KeyValuePairService.CreateOrUpdate("d=a");

            KeyValuePair<string, string>[] pairs = this.KeyValuePairService.GetAll(SortOrder.VALUE)
                                                       .ToArray();
            Assert.AreEqual(4, pairs.Length);

            Assert.AreEqual("a", pairs[0].Value);
            Assert.AreEqual("d", pairs[0].Key);

            Assert.AreEqual("b", pairs[1].Value);
            Assert.AreEqual("c", pairs[1].Key);

            Assert.AreEqual("c", pairs[2].Value);
            Assert.AreEqual("b", pairs[2].Key);

            Assert.AreEqual("d", pairs[3].Value);
            Assert.AreEqual("a", pairs[3].Key);
        }

        #endregion
    }
}