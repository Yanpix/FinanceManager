using System.Linq;
using YanpixFinanceManager.Model.DataAccess.Services;
using YanpixFinanceManager.Model.Entities;

namespace YanpixFinanceManager.Model.DataAccess.Extensions
{
    public static class AccountExtensions
    {
        public static Account ExistingAccount(this IEntityBaseService<Account> accountsRepository, string username, string password)
        {
            return accountsRepository.GetAll()
                .Where(a => a.Username == username && a.Password == password)
                .SingleOrDefault();
        }

        public static Account ExistingAccount(this IEntityBaseService<Account> accountsRepository, string username)
        {
            return accountsRepository.GetAll()
                .Where(a => a.Username == username)
                .SingleOrDefault();
        }
    }
}
