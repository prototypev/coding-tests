namespace KeyValuePairTests.KeyValuePairServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests for ExportAsXml() method.
    /// </summary>
    [TestClass]
    public class ExportAsXmlTests : KeyValuePairServiceTestsBase
    {
        #region Static Fields

        /// <summary>
        /// The XML output file.
        /// </summary>
        private static readonly string XmlOutputFile = "test_" + Guid.NewGuid() + ".xml";

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Called after executing a unit test.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            if (File.Exists(XmlOutputFile))
            {
                File.Delete(XmlOutputFile);
            }
        }

        /// <summary>
        /// Called before executing a unit test.
        /// </summary>
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            Assert.IsFalse(File.Exists(XmlOutputFile));
        }

        /// <summary>
        /// Tests with an empty key-value pair collection.
        /// </summary>
        [TestMethod]
        public void TestWithEmptyData()
        {
            this.KeyValuePairService.ExportAsXml(XmlOutputFile);

            this.AssertXmlContents();
        }

        /// <summary>
        /// Tests with an invalid file name.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestWithInvalidFileName()
        {
            this.KeyValuePairService.ExportAsXml(null);
        }

        /// <summary>
        /// Tests with a non-empty key-value pair collection.
        /// </summary>
        [TestMethod]
        public void TestWithNonEmptyData()
        {
            this.KeyValuePairService.CreateOrUpdate("a=d");
            this.KeyValuePairService.CreateOrUpdate("c=b");
            this.KeyValuePairService.CreateOrUpdate("b=c");
            this.KeyValuePairService.CreateOrUpdate("d=a");

            this.KeyValuePairService.ExportAsXml(XmlOutputFile);

            this.AssertXmlContents();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Asserts the XML contents.
        /// </summary>
        private void AssertXmlContents()
        {
            Assert.IsTrue(File.Exists(XmlOutputFile));

            XDocument document = XDocument.Load(XmlOutputFile);
            Assert.IsNotNull(document.Root);

            Dictionary<string, string> pairs = this.KeyValuePairService.GetAll()
                                                   .ToDictionary(pair => pair.Key, pair => pair.Value);

            XElement[] elements = document.Root.Elements()
                                          .ToArray();
            Assert.AreEqual(pairs.Count, elements.Length);

            foreach (XElement element in elements)
            {
                XElement keyElement = element.Element("key");
                Assert.IsNotNull(keyElement);
                Assert.IsNotNull(keyElement.Value);

                XElement valueElement = element.Element("value");
                Assert.IsNotNull(valueElement);
                Assert.IsNotNull(valueElement.Value);

                string value;
                Assert.IsTrue(pairs.TryGetValue(keyElement.Value, out value));

                Assert.AreEqual(valueElement.Value, value);

                pairs.Remove(keyElement.Value);
            }

            Assert.AreEqual(0, pairs.Count);
        }

        #endregion
    }
}