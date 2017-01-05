//-----------------------------------------------------------------------
// <copyright file="StructureMapDependencyResolver.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;
using Context = System.Web.HttpContext;

namespace Extant.Web.Infrastructure
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        private readonly IContainer _container;

        // context scoping keys for nested containers/special uses.
        private const string ScopedContainerKey = "StructureMapRequestScopedContainer";
        private const string PubmedServiceInstanceKey = "SessionScopedPubmedServiceInstance";

        private IContainer getContainer()
        {
            // detects if a context-scoped nested container is available; if not it creates one.
            // using this methods of scoping allows all registrations to be transient or singleton.

            if (!Context.Current.Items.Contains(ScopedContainerKey)) Context.Current.Items.Add(ScopedContainerKey, _container.GetNestedContainer());

            return (IContainer) Context.Current.Items[ScopedContainerKey];
        }
        

        public StructureMapDependencyResolver(IContainer container)
        {
            _container = container;
        }


        public object GetService(Type serviceType)
        {
            // special handling of IPubMedService due to need to cache results between requests.
            if (serviceType == typeof(Pubmed.IPubmedService))
            {
                if(Context.Current.Session[PubmedServiceInstanceKey] == null) Context.Current.Session.Add(PubmedServiceInstanceKey, _container.GetInstance(typeof(Pubmed.IPubmedService)));
                return Context.Current.Session[PubmedServiceInstanceKey];
            }
            
            IContainer current = getContainer();
            
            if (serviceType == typeof(IContainer)) return current;

            object instance = current.TryGetInstance(serviceType);

            if (instance == null && !serviceType.IsAbstract)
            {
                current.Configure(c => c.AddType(serviceType, serviceType));
                instance = current.TryGetInstance(serviceType);
            }

            return instance;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            // special handling of IPubMedService due to need to cache results between requests.
            if (serviceType == typeof(Pubmed.IPubmedService))
            {
                if (Context.Current.Session[PubmedServiceInstanceKey] == null) Context.Current.Session.Add(PubmedServiceInstanceKey, _container.GetInstance(typeof(Pubmed.IPubmedService)));
                return new List<object> { Context.Current.Session[PubmedServiceInstanceKey] };
            }

            return getContainer().GetAllInstances(serviceType).Cast<object>();
        }

    }
}