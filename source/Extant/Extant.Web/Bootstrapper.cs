//-----------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Configuration;
using System.Security.Principal;
using System.Web.Mvc;
using AutoMapper;
using Extant.Data.Entities;
using Extant.Pubmed;
using Extant.Web.Infrastructure;
using StructureMap;

namespace Extant.Web
{
    public static class Bootstrapper
    {
        public static void Bootstrap()
        {
            var modelBinderTypeMappingDictionary = new ModelBinderTypeMappingDictionary
                                                       {
                                                           {typeof (DateTime), typeof (DateTimeModelBinder)},
                                                           {typeof (FileUpload), typeof (FileUploadModelBinder)},
                                                           {typeof (IPrincipal), typeof (PrincipalModelBinder)}
                                                       };

            var container = new Container();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ExtantMappingProfile>();
                cfg.ConstructServicesUsing(container.GetInstance);
            });

            container.Configure(cfg =>
               {
                   cfg.AddRegistry(new NHibernateRegistry());
                   cfg.AddRegistry(new RepositoryRegistry());
                   cfg.For<IMailer>().Singleton().Use<Mailer>();
                   cfg.For<IPubmedService>().Use<EBIPubmedService>();
                   cfg.For<IModelBinderProvider>().Use<StructureMapModelBinderProvider>();
                   cfg.For<ModelBinderTypeMappingDictionary>().Use(modelBinderTypeMappingDictionary);
               });
                

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));

        }
    }

}