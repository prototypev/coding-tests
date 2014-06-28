// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiControllerBase.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WingtipToys.Controllers.Api
{
    using System.Web.Http;

    using WingtipToys.ActionFilters;

    /// <summary>
    ///     Base class for web API controllers.
    /// </summary>
    [WebApiTransactionActionFilter]
    public abstract class WebApiControllerBase : ApiController
    {
    }
}