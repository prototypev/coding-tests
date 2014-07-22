namespace IncidentReporting
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The incident reporting utility class.
    /// </summary>
    public static class IncidentReporting
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the percentage of time in 'query' in which at least one incident was open.
        /// </summary>
        /// <param name="query">
        /// A 2-item tuple containing the start and end time of the query, respectively.
        /// </param>
        /// <param name="incidents">
        /// A sequence of 2-item tuple. Each 2-item tuple corresponds to the trigger time and resolve time for an incident. Assumption: this sequence is sorted.
        /// </param>
        /// <returns>
        /// The percentage.
        /// </returns>
        public static double PercentageTimeAtLeastOneIncident(Tuple<int, int> query, IList<Tuple<int, int>> incidents)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            int queryStart = query.Item1;
            int queryEnd = query.Item2;

            if (queryStart >= queryEnd)
            {
                throw new ArgumentException("Query end must be > start", "query");
            }

            if (incidents == null)
            {
                throw new ArgumentNullException("incidents");
            }

            double totalIntersection = 0;

            Tuple<int, int> currentInterval = incidents[0];
            for (int i = 1; i < incidents.Count; i++)
            {
                Tuple<int, int> incident = incidents[i];

                if (DoesIncidentsOverlap(currentInterval, incident))
                {
                    // The current interval has an overlap with the current incident, so merge the 2 intervals
                    currentInterval = MergeIncidents(currentInterval, incident);
                }
                else
                {
                    int intersection = GetQueryAndIntervalIntersection(query, currentInterval);
                    totalIntersection += intersection;

                    currentInterval = incident;
                }
            }

            // All done, remember to check the last interval
            int lastIntersection = GetQueryAndIntervalIntersection(query, currentInterval);
            totalIntersection += lastIntersection;

            return totalIntersection / (queryEnd - queryStart);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the intersection of the query and interval.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="interval">The interval.</param>
        /// <returns>The intersection value.</returns>
        private static int GetQueryAndIntervalIntersection(Tuple<int, int> query, Tuple<int, int> interval)
        {
            int queryStart = query.Item1;
            int queryEnd = query.Item2;

            int intervalStart = interval.Item1;
            int intervalEnd = interval.Item2;

            if (intervalStart <= queryEnd && intervalEnd >= queryStart)
            {
                int intersectionStart = Math.Max(queryStart, intervalStart);
                int intersectionEnd = Math.Min(queryEnd, intervalEnd);
                return intersectionEnd - intersectionStart;
            }

            return 0;
        }

        /// <summary>
        /// Determines whether the specified incidents overlap.
        /// </summary>
        /// <param name="incident1">
        /// The first incident.
        /// </param>
        /// <param name="incident2">
        /// The second incident.
        /// </param>
        /// <returns>
        /// <c>true</c> if the 2 incidents overlap; otherwise <c>false</c>.
        /// </returns>
        private static bool DoesIncidentsOverlap(Tuple<int, int> incident1, Tuple<int, int> incident2)
        {
            return incident1.Item2 >= incident2.Item1 && incident2.Item2 >= incident1.Item1;
        }

        /// <summary>
        /// Merges 2 incidents.
        /// </summary>
        /// <param name="incident1">
        /// The first incident.
        /// </param>
        /// <param name="incident2">
        /// The second incident.
        /// </param>
        /// <returns>
        /// The merger of the 2 incidents.
        /// </returns>
        private static Tuple<int, int> MergeIncidents(Tuple<int, int> incident1, Tuple<int, int> incident2)
        {
            int start = Math.Min(incident1.Item1, incident2.Item1);
            int end = Math.Max(incident2.Item2, incident2.Item2);
            return Tuple.Create(start, end);
        }

        #endregion
    }
}