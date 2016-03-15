﻿using FinanceManager.Model.DataAccess.Repository;
using FinanceManager.Model.DataAccess.Services;
using FinanceManager.Model.DataAccess.UnitOfWork;
using FinanceManager.Model.Entities;
using FinanceManager.ViewModel;
using Microsoft.Practices.Unity;

namespace FinanceManager.Infrastructure
{
    public class DependencyResolver
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());

            container.RegisterType(typeof(IDataRepository<>), typeof(DataRepository<>));

            container.RegisterType<IUnitOfWork, UnitOfWork>(
                new InjectionProperty("MoneyBoxesRepository", typeof(IDataRepository<MoneyBox>)),
                new InjectionProperty("CurrenciesRepository", typeof(IDataRepository<Currency>)));

            container.RegisterType(typeof(IDataService<>), typeof(DataService<>), 
                new InjectionProperty("UnitOfWork", typeof(IUnitOfWork)));

            container.RegisterType<MainViewModel>(
                new InjectionProperty("MoneyBoxesDataService", typeof(IDataService<MoneyBox>)), 
                new InjectionProperty("NavigationService", typeof(INavigationService)));

            container.RegisterType<CreateMoneyBoxViewModel>(
                new InjectionProperty("MoneyBoxesDataService", typeof(IDataService<MoneyBox>)),
                new InjectionProperty("NavigationService", typeof(INavigationService)),
                new InjectionProperty("CurrenciesDataService", typeof(IDataService<Currency>)));
        }
    }
}