// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PersistenceTestsBase.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WingtipToys.Core.Test
{
    using FluentNHibernate.Cfg.Db;

    using NUnit.Framework;

    using WingtipToys.Core.Infrastructure.Persistence;

    /// <summary>
    ///     Base class definition for tests involving the persistence layer.
    /// </summary>
    public abstract class PersistenceTestsBase : TestsBase
    {
        #region Methods

        /// <summary>
        ///     Sets up the test fixture.
        /// </summary>
        protected override void FixtureSetup()
        {
            base.FixtureSetup();

            // TODO: Should use a SQLite in-memory database
            SessionFactoryBuilder.Instance.BuildSessionFactory(MsSqlConfiguration.MsSql2008.ConnectionString(@"Server=localhost;Initial Catalog=Wingtiptoys;Integrated Security=SSPI;"));
        }

        /// <summary>
        ///     Tears down the test fixture.
        /// </summary>
        [TestFixtureTearDown]
        protected virtual void FixtureTeardown()
        {
            SessionFactoryBuilder.Instance.SessionFactory.Close();
        }

        /// <summary>
        ///     Sets up the test.
        /// </summary>
        [SetUp]
        protected virtual void Setup()
        {
            SessionManager.Instance.OpenSession();
        }

        /// <summary>
        ///     Tears down the test.
        /// </summary>
        [SetUp]
        protected virtual void Teardown()
        {
            // At the end of the test, do not commit any changes
            SessionManager.Instance.CloseSession(false);
        }

        #endregion
    }
}