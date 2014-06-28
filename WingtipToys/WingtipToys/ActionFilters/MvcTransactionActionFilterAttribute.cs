// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MvcTransactionActionFilterAttribute.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace WingtipToys.ActionFilters
{
    using System;
    using System.Web.Mvc;

    using WingtipToys.Core.Infrastructure.Persistence;

    /// <summary>
    ///     Action filter to support session-per-request design.
    ///     TODO: This is just a quick and dirty way to implement the session-per-request design. In reality, we should use a TransactionScope manager to manage the transaction.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class MvcTransactionActionFilterAttribute : ActionFilterAttribute
    {
        #region Public Methods and Operators

        /// <summary>
        /// Called by the ASP.NET MVC framework after the action method executes.
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            SessionManager.Instance.CloseSession(filterContext.Exception == null);
        }

        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SessionManager.Instance.OpenSession();
        }

        #endregion
    }
}