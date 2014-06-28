// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiTransactionActionFilterAttribute.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WingtipToys.ActionFilters
{
    using System;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    using WingtipToys.Core.Infrastructure.Persistence;

    /// <summary>
    ///     Action filter to support session-per-request design.
    ///     TODO: This is just a quick and dirty way to implement the session-per-request design. In reality, we should use a TransactionScope manager to manage the transaction.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class WebApiTransactionActionFilterAttribute : ActionFilterAttribute
    {
        #region Fields

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Occurs after the action method is invoked.
        /// </summary>
        /// <param name="actionExecutedContext">
        /// The action executed context.
        /// </param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            SessionManager.Instance.CloseSession(actionExecutedContext.Exception == null);
        }

        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">
        /// The action context.
        /// </param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            SessionManager.Instance.OpenSession();
        }

        #endregion
    }
}