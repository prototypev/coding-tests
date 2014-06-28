// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace WingtipToys.Controllers.Mvc
{
    using System.Web.Mvc;

    /// <summary>
    ///     Default controller.
    /// </summary>
    public class HomeController : MvcControllerBase
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Gets the default home view.
        /// </summary>
        /// <returns>The view.</returns>
        [HttpGet]
        public ViewResult Index()
        {
            return this.View();
        }

        #endregion
    }
}