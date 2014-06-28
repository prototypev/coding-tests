// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SessionManager.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WingtipToys.Core.Infrastructure.Persistence
{
    using System.Data;
    using System.Diagnostics.Contracts;
    using System.Web;

    using NHibernate;

    /// <summary>
    ///     The NHibernate session manager.
    /// </summary>
    public class SessionManager
    {
        #region Static Fields

        /// <summary>
        ///     Backing field for Instance.
        /// </summary>
        private static SessionManager instance;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <value>
        ///     The instance.
        /// </value>
        public static SessionManager Instance
        {
            get
            {
                return instance ?? (instance = new SessionManager());
            }
        }

        /// <summary>
        ///     Gets the current session.
        /// </summary>
        /// <value>
        ///     The current session.
        /// </value>
        public ISession CurrentSession
        {
            get
            {
                return HttpContext.Current.Items["NHibernateSession"] as ISession;
            }

            private set
            {
                HttpContext.Current.Items["NHibernateSession"] = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Closes the session.
        /// </summary>
        /// <param name="doCommit">if set to <c>true</c>, commits any changes; otherwise rolls back.</param>
        public void CloseSession(bool doCommit)
        {
            if (this.CurrentSession != null)
            {
                try
                {
                    if (this.CurrentSession.Transaction == null || !this.CurrentSession.Transaction.IsActive || this.CurrentSession.Transaction.WasRolledBack)
                    {
                        this.CurrentSession.Flush();
                    }
                    else
                    {
                        try
                        {
                            if (doCommit)
                            {
                                try
                                {
                                    this.CurrentSession.Transaction.Commit();
                                }
                                catch
                                {
                                    this.CurrentSession.Transaction.Rollback();
                                    throw;
                                }
                            }
                            else
                            {
                                this.CurrentSession.Transaction.Rollback();
                            }
                        }
                        finally
                        {
                            this.CurrentSession.Transaction.Dispose();
                        }
                    }
                }
                finally
                {
                    this.CurrentSession.Dispose();
                    this.CurrentSession = null;
                }
            }
        }

        /// <summary>
        /// Opens the session.
        /// </summary>
        /// <returns>
        /// The <see cref="ISession"/>.
        /// </returns>
        public ISession OpenSession()
        {
            Contract.Ensures(Contract.Result<ISession>() != null);

            if (this.CurrentSession == null)
            {
                this.CurrentSession = SessionFactoryBuilder.Instance.SessionFactory.OpenSession();
                this.CurrentSession.BeginTransaction(IsolationLevel.ReadCommitted);
            }

            return this.CurrentSession;
        }

        #endregion
    }
}