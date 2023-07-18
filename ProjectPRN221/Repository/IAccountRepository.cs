using ProjectPRN221.BusinessObject3;

namespace ManageBookLibrary.Repository
{
    public interface IAccountRepository
    {
        Account GetAccountByEmailAndPass(Account account);
        List<Account> GetAllAccounts();
        Account GetAccountById(int? id);
        void DeleteAccount(int? id);
        void UpdateAccount(Account account);
        void InsertAccount(Account account);
        bool CheckReturnBook(Account account);
        List<Account> SearchAccByNameOrEmailOrPhoneOrAddress(string? search);
        List<Account> GetAccountByRoleId(int? roleId);

    }
}
