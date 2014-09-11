namespace KeyValuePair.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;

    using KeyValuePair.Properties;

    /// <summary>
    /// Concrete implementation of <see cref="IKeyValuePairService"/>.
    /// </summary>
    public class KeyValuePairService : IKeyValuePairService
    {
        #region Fields

        /// <summary>
        /// Simulates the data store containing the key-value pairs.
        /// In a full-blown product, we would replace this with a proper data-access layer.
        /// </summary>
        private readonly IDictionary<string, string> _keyValuePairs = new Dictionary<string, string>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Creates or updates a key-value pair.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <exception cref="ValidationException">Invalid input.</exception>
        public void CreateOrUpdate(string input)
        {
            KeyValuePair<string, string> pair = ParseInput(input);

            this._keyValuePairs[pair.Key] = pair.Value;
        }

        /// <summary>
        /// Deletes the specified items.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public void Delete(IEnumerable<string> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            foreach (string item in items)
            {
                KeyValuePair<string, string> pair = ParseInput(item);
                this._keyValuePairs.Remove(pair.Key);
            }
        }

        /// <summary>
        /// Export the key-value pair data as XML.
        /// </summary>
        /// <param name="fileName">
        /// Name of the file.
        /// </param>
        public void ExportAsXml(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            XmlWriterSettings settings = new XmlWriterSettings
                                             {
                                                 Indent = true
                                             };

            using (XmlWriter writer = XmlWriter.Create(fileName, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("keyValuePairs");

                foreach (KeyValuePair<string, string> pair in this._keyValuePairs)
                {
                    writer.WriteStartElement("keyValuePair");

                    writer.WriteElementString("key", pair.Key);
                    writer.WriteElementString("value", pair.Value);

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        /// <summary>
        /// Gets all key-value pairs.
        /// </summary>
        /// <param name="sortOrder">
        /// The sort order. Defaults to NONE if not specified.
        /// </param>
        /// <returns>
        /// All key-value pairs.
        /// </returns>
        public IEnumerable<KeyValuePair<string, string>> GetAll(SortOrder sortOrder = SortOrder.NONE)
        {
            switch (sortOrder)
            {
                case SortOrder.NAME:
                    return this._keyValuePairs.OrderBy(pair => pair.Key);

                case SortOrder.VALUE:
                    return this._keyValuePairs.OrderBy(pair => pair.Value);

                default:
                    return this._keyValuePairs;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parses the input.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="KeyValuePair"/>.
        /// </returns>
        /// <exception cref="System.ComponentModel.DataAnnotations.ValidationException">
        /// Invalid input.
        /// </exception>
        private static KeyValuePair<string, string> ParseInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ValidationException(Resources.Error_InvalidInput);
            }

            // Allow spaces before/after the =, and at the start/end of the input
            Match match = Regex.Match(input, @"^\s?(?<key>\w+)\s?=\s?(?<value>\w+)\s?$");
            if (!match.Success)
            {
                throw new ValidationException(Resources.Error_InvalidInput);
            }

            string key = match.Groups["key"].Value;
            string value = match.Groups["value"].Value;

            return new KeyValuePair<string, string>(key, value);
        }

        #endregion
    }
}