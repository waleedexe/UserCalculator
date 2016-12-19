using Interfaces;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using SqlRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UserSite.Unity
{
    public static class UnityConfiguration
    {
        private static UnityContainer _container = null;

        public static UnityResolver GetResolver()
        {
            return new UnityResolver(_container);
        }

        public static void ConfigureContainer()
        {
            _container = new UnityContainer();

            _container.RegisterType<IUserService, Services.UserService>(new HierarchicalLifetimeManager());
            _container.RegisterType<IDataValidator, Services.DataValidationService>(new HierarchicalLifetimeManager());
            _container.RegisterType<ICalculator, Services.CalculatorService>(new HierarchicalLifetimeManager());
            _container.RegisterType(typeof(IEntityRepository<>), typeof(EntityRepository<>));

            // Also register for Mvc resolver..
            RegisterMvcResolver();
        }

        public static void RegisterMvcResolver()
        {
            DependencyResolver.SetResolver(new UnityDependencyResolver(_container));
        }
    }
}
