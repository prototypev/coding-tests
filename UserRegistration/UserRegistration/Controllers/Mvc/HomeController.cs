namespace UserRegistration.Controllers.Mvc
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using UserRegistration.Core.Entities;
    using UserRegistration.Core.Persistence;
    using UserRegistration.Core.Services;

    /// <summary>
    /// Main application controller.
    /// </summary>
    public class HomeController : Controller
    {
        #region Fields

        /// <summary>
        /// The user registration service.
        /// For more complex application, we would use IoC to inject the service.
        /// </summary>
        private readonly IUserRegistrationService _userRegistrationService = new UserRegistrationService(new DatabaseContext());

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Renders the user registration view.
        /// </summary>
        /// <returns>The user registration view.</returns>
        [HttpGet]
        [AllowAnonymous]
        public ViewResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="user">
        /// The user information to register.
        /// </param>
        /// <returns>
        /// Returns back to the user registration view if a problem was encountered; otherwise redirects to a success page.
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        public ViewResult RegisterUser(User user)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    this._userRegistrationService.RegisterUser(user);
                    return this.View("RegistrationSuccess");
                }
                catch (ValidationException ex)
                {
                    this.ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return this.View("Index", user);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Releases unmanaged resources and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources; false to release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (this._userRegistrationService != null)
            {
                this._userRegistrationService.Dispose();
            }
        }

        #endregion
    }
}