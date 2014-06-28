// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SessionFactoryBuilder.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace WingtipToys.Core.Infrastructure.Persistence
{
    using System.Diagnostics.Contracts;
    using System.Reflection;
    using System.Threading;

    using FluentNHibernate.Automapping;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using FluentNHibernate.Conventions.Helpers;

    using NHibernate;
    using NHibernate.Cfg;

    /// <summary>
    ///     The NHibernate session factory builder.
    ///     TODO: Should use IoC to export the session factory.
    /// </summary>
    public class SessionFactoryBuilder
    {
        #region Static Fields

        /// <summary>
        ///     The manual reset event for signaling when the session factory has finished building.
        /// </summary>
        private static readonly ManualResetEvent ManualResetEvent = new ManualResetEvent(false);

        /// <summary>
        ///     The synchronize lock.
        /// </summary>
        private static readonly object SyncLock = new object();

        /// <summary>
        ///     Backing field for Instance.
        /// </summary>
        private static SessionFactoryBuilder instance;

        #endregion

        #region Fields

        /// <summary>
        ///     Backing field for SessionFactory.
        /// </summary>
        private ISessionFactory sessionFactory;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <value>
        ///     The instance.
        /// </value>
        public static SessionFactoryBuilder Instance
        {
            get
            {
                return instance ?? (instance = new SessionFactoryBuilder());
            }
        }

        /// <summary>
        ///     Gets the session factory.
        /// </summary>
        /// <value>
        ///     The session factory.
        /// </value>
        public ISessionFactory SessionFactory
        {
            get
            {
                WaitUntilSessionFactoryBuilt();

                return this.sessionFactory;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Determines whether the session factory has finished building.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the session factory has finished building; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSessionFactoryBuilt()
        {
            return ManualResetEvent.WaitOne(0);
        }

        /// <summary>
        ///     Waits until the session factory is built.
        /// </summary>
        public static void WaitUntilSessionFactoryBuilt()
        {
            // Block here until the session factory has finished building
            ManualResetEvent.WaitOne();
        }

        /// <summary>
        /// Builds the session factory.
        /// </summary>
        /// <param name="persistenceConfigurer">The persistence configurer.</param>
        /// <returns>
        /// The session factory.
        /// </returns>
        public ISessionFactory BuildSessionFactory(IPersistenceConfigurer persistenceConfigurer)
        {
            Contract.Requires(persistenceConfigurer != null);
            Contract.Ensures(Contract.Result<ISessionFactory>() != null);

            // Building a session factory takes some time, so ensure that this call is thread safe
            if (!ManualResetEvent.WaitOne(0))
            {
                lock (SyncLock)
                {
                    if (!ManualResetEvent.WaitOne(0))
                    {
                        Configuration configuration = Fluently.Configure().Mappings(config => config.AutoMappings.Add(CreateAutoPersistenceModel())).Database(persistenceConfigurer).BuildConfiguration();

                        this.sessionFactory = configuration.BuildSessionFactory();
                        ManualResetEvent.Set();
                    }
                }
            }

            return this.sessionFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Creates the auto persistence model.
        /// </summary>
        /// <returns>
        ///     The newly created auto persistence model.
        /// </returns>
        private static AutoPersistenceModel CreateAutoPersistenceModel()
        {
            var autoPersistenceModel = new AutoPersistenceModel(new AutomappingConfiguration()) { MergeMappings = true };

            // setup our conventions
            autoPersistenceModel.Conventions.Setup(conventions =>
                {
                    conventions.Add(DefaultLazy.Always());
                    conventions.Add(DefaultCascade.SaveUpdate());
                });

            Assembly assembly = Assembly.GetExecutingAssembly();
            autoPersistenceModel.AddEntityAssembly(assembly);
            autoPersistenceModel.UseOverridesFromAssembly(assembly);

            return autoPersistenceModel;
        }

        #endregion
    }
}