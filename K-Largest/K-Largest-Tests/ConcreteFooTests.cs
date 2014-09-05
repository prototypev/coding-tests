namespace K_Largest_Tests
{
    using System;
    using System.Collections.Generic;

    using K_Largest;

    using NUnit.Framework;

    /// <summary>
    /// Tests for ConcreteFoo.
    /// </summary>
    public abstract class ConcreteFooTests
    {
        /// <summary>
        /// Tests for the ConcreteFoo constructor.
        /// </summary>
        [TestFixture]
        public class ConstructorTests : ConcreteFooTests
        {
            #region Public Methods and Operators

            /// <summary>
            /// Tests with a negative K value, and expects an ArgumentOutOfRangeException.
            /// </summary>
            [Test]
            public void Test_WithNegativeK_ExpectsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new ConcreteFoo<string>(-1), "Constructor should throw ArgumentOutOfRangeException with a negative K value.");
            }

            /// <summary>
            /// Tests with a non-negative K value, and expects everything to be ok.
            /// </summary>
            [Test]
            public void Test_WithNonNegativeK_ExpectsOK()
            {
                Assert.DoesNotThrow(() => new ConcreteFoo<string>(0), "Constructor should not throw exception with a non-negative K value.");
            }

            #endregion
        }

        /// <summary>
        /// Tests for the GetTopK() method.
        /// </summary>
        [TestFixture]
        public class GetTopKTests : ConcreteFooTests
        {
            #region Public Methods and Operators

            /// <summary>
            /// Tests with different calls to Offer() with different items, and expects a properly sorted list.
            /// </summary>
            [Test]
            public void Test_WithDifferentOffers_ExpectsSortedList()
            {
                Foo<string> foo = new ConcreteFoo<string>(3);

                foo.Offer("b");
                foo.Offer("c");
                foo.Offer("a");

                List<string> topK = foo.GetTopK();
                Assert.IsNotNull(topK, "GetTopK() should not return null.");
                Assert.AreEqual(3, topK.Count, "K is 3, and Offer() called 3x, so GetTopK() should return a list of size 3.");
                Assert.AreEqual("c", topK[0], "First item in the list should match largest value provided to Offer().");
                Assert.AreEqual("b", topK[1], "Second item in the list should match middle value provided to Offer().");
                Assert.AreEqual("a", topK[2], "Third item in the list should match smaller value provided to Offer().");
            }

            /// <summary>
            /// Tests after calling Offer() N times (where N is less than K), and expects a list of size N.
            /// </summary>
            [Test]
            public void Test_WithLessOffersThanK_ExpectsListOfCorrectSize()
            {
                Foo<string> foo = new ConcreteFoo<string>(2);

                foo.Offer("a");

                List<string> topK = foo.GetTopK();
                Assert.IsNotNull(topK, "GetTopK() should not return null.");
                Assert.AreEqual(1, topK.Count, "Offer() only called once, so GetTopK() should return a list of size 1.");
                Assert.AreEqual("a", topK[0], "Item returned should match what was provided to Offer().");
            }

            /// <summary>
            /// Tests after calling Offer() N times (where N is greater than K), and expects a list of size K.
            /// </summary>
            [Test]
            public void Test_WithMoreOffersThanK_ExpectsListOfCorrectSize()
            {
                Foo<string> foo = new ConcreteFoo<string>(1);

                foo.Offer("b");
                foo.Offer("a");

                List<string> topK = foo.GetTopK();
                Assert.IsNotNull(topK, "GetTopK() should not return null.");
                Assert.AreEqual(1, topK.Count, "K is 1, so GetTopK() should return a list of size 1.");
                Assert.AreEqual("b", topK[0], "Item returned should match largest value provided to Offer().");
            }

            /// <summary>
            /// Tests without calling Offer() beforehand, and expects an empty list.
            /// </summary>
            [Test]
            public void Test_WithNoOffers_ExpectsEmptyList()
            {
                Foo<string> foo = new ConcreteFoo<string>(1);

                List<string> topK = foo.GetTopK();
                Assert.IsNotNull(topK, "GetTopK() should not return null.");
                Assert.IsEmpty(topK, "GetTopK() should return an empty list.");
            }

            /// <summary>
            /// Tests calling Offer() after GetTopK(), and expects a properly sorted list.
            /// This is the exact use case as provided by the question.
            /// </summary>
            [Test]
            public void Test_WithOfferAfterGetTopK_ExpectsSortedList()
            {
                Foo<string> foo = new ConcreteFoo<string>(3);

                List<string> topK = foo.GetTopK();
                Assert.IsNotNull(topK, "GetTopK() should not return null.");
                Assert.IsEmpty(topK, "GetTopK() should return an empty list.");

                foo.Offer("a");
                topK = foo.GetTopK();
                Assert.AreEqual(1, topK.Count, "K is 3, and Offer() called 1x, so GetTopK() should return a list of size 1.");
                Assert.AreEqual("a", topK[0], "Item returned should match what was provided to Offer().");

                foo.Offer("b");
                topK = foo.GetTopK();
                Assert.AreEqual(2, topK.Count, "K is 3, and Offer() called 2x, so GetTopK() should return a list of size 2.");
                Assert.AreEqual("b", topK[0], "First item in the list should match larger value provided to Offer() up until now.");
                Assert.AreEqual("a", topK[1], "Second item in the list should match smaller value provided to Offer() up until now.");

                foo.Offer("aa");
                topK = foo.GetTopK();
                Assert.AreEqual(3, topK.Count, "K is 3, and Offer() called 3x, so GetTopK() should return a list of size 3.");
                Assert.AreEqual("b", topK[0], "First item in the list should match largest value provided to Offer() up until now.");
                Assert.AreEqual("aa", topK[1], "Second item in the list should match second largest value provided to Offer() up until now.");
                Assert.AreEqual("a", topK[2], "Third item in the list should match third largest value provided to Offer() up until now.");

                foo.Offer("c");
                topK = foo.GetTopK();
                Assert.AreEqual(3, topK.Count, "K is 3, and Offer() called 4x, so GetTopK() should return a list of size 3.");
                Assert.AreEqual("c", topK[0], "First item in the list should match largest value provided to Offer() up until now.");
                Assert.AreEqual("b", topK[1], "Second item in the list should match second largest value provided to Offer() up until now.");
                Assert.AreEqual("aa", topK[2], "Third item in the list should match third largest value provided to Offer() up until now.");

                foo.Offer("a");
                topK = foo.GetTopK();
                Assert.AreEqual(3, topK.Count, "K is 3, and Offer() called 5x, so GetTopK() should return a list of size 3.");
                Assert.AreEqual("c", topK[0], "First item in the list should match largest value provided to Offer() up until now.");
                Assert.AreEqual("b", topK[1], "Second item in the list should match second largest value provided to Offer() up until now.");
                Assert.AreEqual("aa", topK[2], "Third item in the list should match third largest value provided to Offer() up until now.");

                foo.Offer("d");
                topK = foo.GetTopK();
                Assert.AreEqual(3, topK.Count, "K is 3, and Offer() called 6x, so GetTopK() should return a list of size 3.");
                Assert.AreEqual("d", topK[0], "First item in the list should match largest value provided to Offer() up until now.");
                Assert.AreEqual("c", topK[1], "Second item in the list should match second largest value provided to Offer() up until now.");
                Assert.AreEqual("b", topK[2], "Third item in the list should match third largest value provided to Offer() up until now.");
            }

            /// <summary>
            /// Tests with repeated calls to Offer() with the same item, and expects the list to contain the repeats.
            /// </summary>
            [Test]
            public void Test_WithRepeatedOffers_ExpectsListToContainRepeats()
            {
                Foo<string> foo = new ConcreteFoo<string>(2);

                foo.Offer("a");
                foo.Offer("a");

                List<string> topK = foo.GetTopK();
                Assert.IsNotNull(topK, "GetTopK() should not return null.");
                Assert.AreEqual(2, topK.Count, "K is 2, and Offer() called 2x, so GetTopK() should return a list of size 2.");
                Assert.AreEqual("a", topK[0], "First item in the list should match what was provided to Offer().");
                Assert.AreEqual("a", topK[1], "Second item in the list should match what was provided to Offer().");
            }

            #endregion
        }

        /// <summary>
        /// Tests for the Offer() method.
        /// </summary>
        [TestFixture]
        public class OfferTests : ConcreteFooTests
        {
            #region Public Methods and Operators

            /// <summary>
            /// Tests with a non-null value, and expects everything to be ok.
            /// </summary>
            [Test]
            public void Test_WithNonNullValue_ExpectsOK()
            {
                Foo<string> foo = new ConcreteFoo<string>(1);

                Assert.DoesNotThrow(() => foo.Offer(string.Empty));
            }

            /// <summary>
            /// Tests with a null value, and expects an ArgumentNullException.
            /// </summary>
            [Test]
            public void Test_WithNullValue_ExpectsArgumentNullException()
            {
                Foo<string> foo = new ConcreteFoo<string>(1);

                Assert.Throws<ArgumentNullException>(() => foo.Offer(null));
            }

            #endregion
        }
    }
}