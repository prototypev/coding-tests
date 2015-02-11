namespace UserRegistration.Core.Services
{
    using System;

    using UserRegistration.Core.Entities;

    /// <summary>
    /// Defines the services associated with user registration.
    /// </summary>
    public interface IUserRegistrationService : IDisposable
    {
        #region Public Methods and Operators

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="user">The user information to register.</param>
        /// <returns>The newly registered user.</returns>
        User RegisterUser(User user);

        #endregion
    }
}