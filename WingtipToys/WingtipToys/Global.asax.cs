// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="">
//   Copyright (c) 2013 Aaron Zhang, for OrderDynamics coding test.  All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace WingtipToys
{
    using System.Configuration;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using FluentNHibernate.Cfg.Db;

    using WingtipToys.Core.Infrastructure.Persistence;

    /// <summary>
    ///     Note: For instructions on enabling IIS6 or IIS7 classic mode,
    ///     visit http://go.microsoft.com/?LinkId=9394801
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        #region Methods

        /// <summary>
        ///     Occurs when the application ends.
        /// </summary>
        protected void Application_End()
        {
            SessionFactoryBuilder.Instance.SessionFactory.Close();
        }

        /// <summary>
        ///     Occurs when the application starts.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterBundles(BundleTable.Bundles);

            BuildSessionFactory();
        }

        /// <summary>
        ///     Builds the session factory.
        /// </summary>
        private static void BuildSessionFactory()
        {
            // Read connection string from web.config
            string connectionString = ConfigurationManager.ConnectionStrings["WingtipToysDB"].ConnectionString;

            // TODO: Instead of hard-coding to MSSQL 2008, should make it configurable
            SessionFactoryBuilder.Instance.BuildSessionFactory(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString));
        }

        /// <summary>
        /// Registers the bundles.
        /// </summary>
        /// <param name="bundles">
        /// The bundles.
        /// </param>
        private static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Models").IncludeDirectory("~/Models", "*.js", true));
            bundles.Add(new ScriptBundle("~/Scripts/ThirdParty").Include("~/Scripts/jquery-2.0.3.js", "~/Scripts/underscore.js", "~/Scripts/backbone.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Core").IncludeDirectory("~/Scripts/Core", "*.js", true));

            bundles.Add(new StyleBundle("~/Content").IncludeDirectory("~/Content", "*.css", true));
        }

        /// <summary>
        /// Registers the global filters.
        /// </summary>
        /// <param name="filters">
        /// The filters.
        /// </param>
        private static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapHttpRoute("ActionApi", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional });

            routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });

            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        #endregion
    }
}