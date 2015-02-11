namespace UserRegistration.Core.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using UserRegistration.Core.Validation;

    /// <summary>
    /// Models a User object.
    /// </summary>
    [Table("User")]
    public class User : Entity
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [PasswordValidation]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Display(Name = "User Name")]
        [DataType(DataType.Text)]
        [UserNameValidation]
        public string UserName { get; set; }

        #endregion
    }
}