using ManageBookLibrary.BusinessObject;
using ManageBookLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageBookLibrary.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public bool CheckReturnBook(Account account) => AccountDAO.Instance.CheckReturnBook(account);

        public void DeleteAccount(int? id) => AccountDAO.Instance.DeleteAccount(id);

        public Account GetAccountByEmailAndPass(Account account) => AccountDAO.Instance.GetAccountByEmailAndPass(account);

        public Account GetAccountById(int? id) => AccountDAO.Instance.GetAccountByID(id);

        public List<Account> GetAccountByRoleId(int? roleId) => AccountDAO.Instance.GetAccountByRoleId(roleId);

        public List<Account> GetAllAccounts() => AccountDAO.Instance.GetAllAccounts();


        public void InsertAccount(Account account) => AccountDAO.Instance.AddNew(account);

        public List<Account> SearchAccByNameOrEmailOrPhoneOrAddress(string? search) => AccountDAO.Instance.SearchAccByNameOrEmailOrPhoneOrAddress(search);

        public void UpdateAccount(Account account) => AccountDAO.Instance.Update(account);
    }
}
