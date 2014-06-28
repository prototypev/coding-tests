// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MvcControllerBase.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WingtipToys.Controllers.Mvc
{
    using System.Web.Mvc;

    using WingtipToys.ActionFilters;

    /// <summary>
    ///     Base class for MVC controller.
    /// </summary>
    [MvcTransactionActionFilter]
    public abstract class MvcControllerBase : Controller
    {
    }
}