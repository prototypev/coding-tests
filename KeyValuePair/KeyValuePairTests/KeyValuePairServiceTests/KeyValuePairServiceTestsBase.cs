namespace KeyValuePairTests.KeyValuePairServiceTests
{
    using KeyValuePair.Services;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Base class definition for <see cref="KeyValuePairService"/> tests.
    /// </summary>
    public abstract class KeyValuePairServiceTestsBase
    {
        #region Properties

        /// <summary>
        /// Gets the key-value pair service.
        /// </summary>
        /// <value>
        /// The key-value pair service.
        /// </value>
        protected IKeyValuePairService KeyValuePairService { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Called before executing a unit test.
        /// </summary>
        [TestInitialize]
        public virtual void TestInitialize()
        {
            this.KeyValuePairService = new KeyValuePairService();
        }

        #endregion
    }
}