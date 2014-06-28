// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestsBase.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WingtipToys.Core.Test
{
    using System.IO;
    using System.Web;

    using NUnit.Framework;

    /// <summary>
    ///     Base class definition for NUnit tests.
    /// </summary>
    public abstract class TestsBase
    {
        #region Methods

        /// <summary>
        ///     Sets up the test fixture.
        /// </summary>
        [TestFixtureSetUp]
        protected virtual void FixtureSetup()
        {
            // Mock HttpContext. See: http://stackoverflow.com/questions/4379450/mock-httpcontext-current-in-test-init-method
            HttpContext.Current = new HttpContext(new HttpRequest(string.Empty, "http://localhost", string.Empty), new HttpResponse(new StringWriter()));
        }

        #endregion
    }
}