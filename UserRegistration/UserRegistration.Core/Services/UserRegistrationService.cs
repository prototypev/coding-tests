namespace UserRegistration.Core.Services
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using UserRegistration.Core.Entities;
    using UserRegistration.Core.Persistence;

    /// <summary>
    /// Implementation for the services associated with user registration.
    /// </summary>
    public class UserRegistrationService : IUserRegistrationService
    {
        #region Fields

        /// <summary>
        /// The database context.
        /// </summary>
        private readonly DatabaseContext _databaseContext;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRegistrationService"/> class.
        /// </summary>
        /// <param name="databaseContext">
        /// The database context.
        /// </param>
        public UserRegistrationService(DatabaseContext databaseContext)
        {
            if (databaseContext == null)
            {
                throw new ArgumentNullException("databaseContext");
            }

            this._databaseContext = databaseContext;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="user">
        /// The user information to register.
        /// </param>
        /// <returns>
        /// The newly registered user.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// A valid User must be provided
        /// </exception>
        public User RegisterUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user", "A valid User must be provided");
            }

            user.Validate();

            if (this._databaseContext.Users.Any(existingUser => existingUser.UserName == user.UserName))
            {
                throw new ValidationException(string.Format("A user with the user name {0} already exists", user.UserName));
            }

            this._databaseContext.Users.Add(user);
            this._databaseContext.SaveChanges();

            return user;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (this._databaseContext != null)
                {
                    this._databaseContext.Dispose();
                }
            }
        }

        #endregion
    }
}