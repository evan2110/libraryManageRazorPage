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

        public Account GetAccountByEmailAndPass(Account account) => AccountDAO.Instance.GetAccountByEmailAndPass(account);

        public void InsertAccount(Account account) => AccountDAO.Instance.AddNew(account);

        public void UpdateAccount(Account account) => AccountDAO.Instance.Update(account);
    }
}
