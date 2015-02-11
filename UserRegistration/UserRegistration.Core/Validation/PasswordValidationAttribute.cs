namespace UserRegistration.Core.Validation
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Password validation.
    /// </summary>
    public class PasswordValidationAttribute : StringLengthAttribute
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordValidationAttribute"/> class.
        /// </summary>
        public PasswordValidationAttribute()
            : base(int.MaxValue)
        {
            // Could easily make this configurable
            this.MinimumLength = 8;
            this.ErrorMessage = "Password must be at least 8 characters, and contain at least 1 number, 1 uppercase, and 1 lowercase character";
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

            string passwordString = value as string;
            return !string.IsNullOrWhiteSpace(passwordString) &&
                   Regex.IsMatch(passwordString, "[0-9]") &&
                   Regex.IsMatch(passwordString, "[a-z]") &&
                   Regex.IsMatch(passwordString, "[A-Z]");
        }

        #endregion
    }
}