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

            IList<Tuple<int, int>> nonOverlappingIncidents = RemoveOverlappingIncidents(incidents);

            foreach (Tuple<int, int> incident in nonOverlappingIncidents)
            {
                int incidentStart = incident.Item1;
                int incidentEnd = incident.Item2;
                if (incidentStart <= queryEnd && incidentEnd >= queryStart)
                {
                    int intersectionStart = Math.Max(queryStart, incidentStart);
                    int intersectionEnd = Math.Min(queryEnd, incidentEnd);
                    totalIntersection += intersectionEnd - intersectionStart;
                }
            }

            return totalIntersection / (queryEnd - queryStart);
        }

        /// <summary>
        /// Removes the overlapping incidents.
        /// </summary>
        /// <param name="incidents">
        /// A sequence of 2-item tuple. Each 2-item tuple corresponds to the trigger time and resolve time for an incident. Assumption: this sequence is sorted.
        /// </param>
        /// <returns>
        /// The sequence of 2-item tuples, with overlaps removed.
        /// </returns>
        public static IList<Tuple<int, int>> RemoveOverlappingIncidents(IList<Tuple<int, int>> incidents)
        {
            if (incidents == null)
            {
                throw new ArgumentNullException("incidents");
            }

            if (incidents.Count <= 1)
            {
                // Optimization for case of 0 or 1 incidents
                return incidents;
            }

            IList<Tuple<int, int>> nonOverlappingIncidents = new List<Tuple<int, int>>();

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
                    nonOverlappingIncidents.Add(currentInterval);

                    currentInterval = incident;
                }
            }

            // All done, remember to add the last interval
            nonOverlappingIncidents.Add(currentInterval);

            return nonOverlappingIncidents;
        }

        #endregion

        #region Methods

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