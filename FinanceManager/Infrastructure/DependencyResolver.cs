using FinanceManager.Model.DataAccess.Providers;
using FinanceManager.Model.DataAccess.Repository;
using FinanceManager.Model.DataAccess.Services;
using FinanceManager.Model.DataAccess.UnitOfWork;
using FinanceManager.Model.Entities;
using FinanceManager.ViewModel;
using Microsoft.Practices.Unity;
using System.ComponentModel;

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
                new InjectionProperty("CategoriesRepository", typeof(IDataRepository<Category>)),
                new InjectionProperty("CurrenciesRepository", typeof(IDataRepository<Currency>)),
                new InjectionProperty("UsersRepository", typeof(IDataRepository<User>)),
                new InjectionProperty("TransactionsRepository", typeof(IDataRepository<Transaction>)));

            container.RegisterType(typeof(IDataService<>), typeof(DataService<>), 
                new InjectionProperty("UnitOfWork", typeof(IUnitOfWork)));

            container.RegisterType<IDataServicesProvider, DataServicesProvider>(
                new InjectionProperty("MoneyBoxesDataService", typeof(IDataService<MoneyBox>)),
                new InjectionProperty("TransactionsDataService", typeof(IDataService<Transaction>)),
                new InjectionProperty("CategoriesDataService", typeof(IDataService<Category>)),
                new InjectionProperty("CurrenciesDataService", typeof(IDataService<Currency>)),
                new InjectionProperty("UsersDataService", typeof(IDataService<User>)));

            container.RegisterType<MainViewModel>(
                new InjectionProperty("NavigationService", typeof(INavigationService)),
                new InjectionProperty("DataService", typeof(IDataServicesProvider)));

            container.RegisterType<CreateMoneyBoxViewModel>(
                new InjectionProperty("NavigationService", typeof(INavigationService)),
                new InjectionProperty("DataService", typeof(IDataServicesProvider)));

            container.RegisterType<MoneyBoxViewModel>(
                new InjectionProperty("NavigationService", typeof(INavigationService)),
                new InjectionProperty("DataService", typeof(IDataServicesProvider)));

            container.RegisterType<CreateTransactionViewModel>(
                new InjectionProperty("NavigationService", typeof(INavigationService)),
                new InjectionProperty("DataService", typeof(IDataServicesProvider)));

            container.RegisterType<CreateCategoryViewModel>(
                new InjectionProperty("NavigationService", typeof(INavigationService)),
                new InjectionProperty("DataService", typeof(IDataServicesProvider)));

            container.RegisterType<CreateCurrencyViewModel>(
                new InjectionProperty("NavigationService", typeof(INavigationService)),
                new InjectionProperty("DataService", typeof(IDataServicesProvider)));

            container.RegisterType<CreateUserViewModel>(
                new InjectionProperty("NavigationService", typeof(INavigationService)),
                new InjectionProperty("DataService", typeof(IDataServicesProvider)));

            container.RegisterType<CalculatorViewModel>(
                new InjectionProperty("NavigationService", typeof(INavigationService)));

            container.RegisterType<CategoryViewModel>(
                new InjectionProperty("NavigationService", typeof(INavigationService)),
                new InjectionProperty("DataService", typeof(IDataServicesProvider)));
        }
    }
}
