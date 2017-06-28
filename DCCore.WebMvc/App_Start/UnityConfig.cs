using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using DCCore.Domain;
using Microsoft.AspNet.Identity;
using DCCore.WebMvc.Identity;
using Microsoft.Practices.Unity.Mvc;
using System.Web.Mvc;
using DCCore.EntityFramework;

namespace DCCore.WebMvc.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager(), new InjectionConstructor("Development"));
            container.RegisterType<IUserStore<IdentityUser, Guid>, UserStore>(new TransientLifetimeManager());
            container.RegisterType<RoleStore>(new TransientLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
