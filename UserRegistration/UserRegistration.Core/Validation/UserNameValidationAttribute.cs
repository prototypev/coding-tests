namespace UserRegistration.Core.Validation
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    /// <summary>
    /// User name validation.
    /// </summary>
    public class UserNameValidationAttribute : StringLengthAttribute
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNameValidationAttribute"/> class.
        /// </summary>
        public UserNameValidationAttribute()
            : base(int.MaxValue)
        {
            // Could easily make this configurable
            this.MinimumLength = 5;
            this.ErrorMessage = "User name must be at least 5 characters";
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Determines whether a specified object is valid.
        /// </summary>
        /// <param name="value">
        /// The object to validate.
        /// </param>
        /// <returns>
        /// true if the specified object is valid; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (!base.IsValid(value))
            {
                return false;
            }

            string userName = value as string;
            return !string.IsNullOrWhiteSpace(userName) &&
                   Regex.IsMatch(userName, "^[a-zA-Z0-9]*$");
        }

        #endregion
    }
}