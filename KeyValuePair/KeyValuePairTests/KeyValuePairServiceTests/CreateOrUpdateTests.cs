namespace KeyValuePairTests.KeyValuePairServiceTests
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the CreateOrUpdate() method.
    /// </summary>
    [TestClass]
    public class CreateOrUpdateTests : KeyValuePairServiceTestsBase
    {
        #region Public Methods and Operators

        /// <summary>
        /// Tests with an input that has an invalid format.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestWithInvalidInputFormat()
        {
            this.KeyValuePairService.CreateOrUpdate("a=a=a");
        }

        /// <summary>
        /// Tests with an input that has an non-alphanumeric characters.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestWithNonAlphanumericInput()
        {
            this.KeyValuePairService.CreateOrUpdate("a=@");
        }

        /// <summary>
        /// Tests with a null input.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TestWithNullInput()
        {
            this.KeyValuePairService.CreateOrUpdate(null);
        }

        /// <summary>
        /// Tests with inputs that have valid format.
        /// </summary>
        [TestMethod]
        public void TestWithValidFormat()
        {
            this.KeyValuePairService.CreateOrUpdate("a1=a1");
            this.KeyValuePairService.CreateOrUpdate(" a2 = a2 ");
        }

        #endregion
    }
}