﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QTec.Hrms.Web.App_Start
{
    using System.Web.Http;

    using Ninject;

    using QTec.Hrms.DataTier;
    using QTec.Hrms.DataTier.Contracts;
    using QTec.Hrms.DataTier.Repositories;

    public class IocConfig
    {
        /// <summary>
        /// The register ioc.
        /// </summary>
        /// <param name="configuration">
        /// The configuration.
        /// </param>
        public static void RegisterIoc(HttpConfiguration configuration)
        {
            var kernel = new StandardKernel();
            kernel.Bind<RepositoryFactories>().To<RepositoryFactories>().InSingletonScope();
            kernel.Bind<IRepositoryProvider>().To<RepositoryProvider>();
            kernel.Bind<IQTecUnitOfWork>().To<QTecUnitOfWork>();
            configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
        }
    }
}