namespace UserRegistration.Core.Persistence
{
    using System.Data.Entity;

    using UserRegistration.Core.Entities;

    /// <summary>
    /// The database context.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public virtual IDbSet<User> Users { get; set; }

        #endregion
    }
}