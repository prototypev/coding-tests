namespace IncidentReporting
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    /// <summary>
    /// The incident reporting tests.
    /// </summary>
    public abstract class IncidentReportingTests
    {
        /// <summary>
        /// Tests for PercentageTimeAtLeastOneIncident().
        /// </summary>
        [TestFixture]
        public class PercentageTimeAtLeastOneIncidentTests : IncidentReportingTests
        {
            #region Public Methods and Operators

            /// <summary>
            /// Tests with invalid incidents input.
            /// </summary>
            [Test]
            public void TestWithInvalidIncidents()
            {
                Assert.Throws<ArgumentNullException>(() => IncidentReporting.PercentageTimeAtLeastOneIncident(Tuple.Create(0, 1), null));
            }

            /// <summary>
            /// Tests with invalid query input.
            /// </summary>
            [Test]
            public void TestWithInvalidQuery()
            {
                IList<Tuple<int, int>> incidents = new Tuple<int, int>[0];

                Assert.Throws<ArgumentNullException>(() => IncidentReporting.PercentageTimeAtLeastOneIncident(null, incidents));
                Assert.Throws<ArgumentException>(() => IncidentReporting.PercentageTimeAtLeastOneIncident(Tuple.Create(0, 0), incidents));
            }

            /// <summary>
            /// Tests with no incident intersection against the query.
            /// </summary>
            [Test]
            public void TestWithNoIncidentIntersection()
            {
                Tuple<int, int> query = Tuple.Create(0, 7);
                IList<Tuple<int, int>> incidents = new[]
                                                       {
                                                           Tuple.Create(8, 15), 
                                                           Tuple.Create(18, 19)
                                                       };

                Assert.AreEqual(0, IncidentReporting.PercentageTimeAtLeastOneIncident(query, incidents));
            }

            /// <summary>
            /// Tests with no overlapping incidents. I.e. Example 1.
            /// </summary>
            [Test]
            public void TestWithNoOverlappingIncidents()
            {
                Tuple<int, int> query = Tuple.Create(10, 20);
                IList<Tuple<int, int>> incidents = new[]
                                                       {
                                                           Tuple.Create(8, 15), 
                                                           Tuple.Create(18, 19)
                                                       };

                Assert.AreEqual(0.6, IncidentReporting.PercentageTimeAtLeastOneIncident(query, incidents));
            }

            /// <summary>
            /// Tests with overlapping incidents. I.e. Example 2.
            /// </summary>
            [Test]
            public void TestWithOverlappingIncidents()
            {
                Tuple<int, int> query = Tuple.Create(40, 70);
                IList<Tuple<int, int>> incidents = new[]
                                                       {
                                                           Tuple.Create(50, 60), 
                                                           Tuple.Create(50, 62), 
                                                           Tuple.Create(50, 65)
                                                       };

                Assert.AreEqual(0.5, IncidentReporting.PercentageTimeAtLeastOneIncident(query, incidents));
            }

            #endregion
        }
    }
}