namespace KeyValuePair.Services
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Definition for key-value pair service properties and methods.
    /// </summary>
    public interface IKeyValuePairService
    {
        #region Public Methods and Operators

        /// <summary>
        /// Creates or updates a key-value pair.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <exception cref="ValidationException">
        /// Invalid input.
        /// </exception>
        void CreateOrUpdate(string input);

        /// <summary>
        /// Deletes the specified items.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        void Delete(IEnumerable<string> items);

        /// <summary>
        /// Export the key-value pair data as XML.
        /// </summary>
        /// <param name="fileName">
        /// Name of the file.
        /// </param>
        void ExportAsXml(string fileName);

        /// <summary>
        /// Gets all key-value pairs.
        /// </summary>
        /// <param name="sortOrder">
        /// The sort order. Defaults to NONE if not specified.
        /// </param>
        /// <returns>
        /// All key-value pairs.
        /// </returns>
        IEnumerable<KeyValuePair<string, string>> GetAll(SortOrder sortOrder = SortOrder.NONE);

        #endregion
    }
}