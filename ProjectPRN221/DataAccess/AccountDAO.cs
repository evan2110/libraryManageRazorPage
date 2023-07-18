using ProjectPRN221.BusinessObject3;

namespace ManageBookLibrary.DataAccess
{
    public class AccountDAO
    {
        private static AccountDAO instance = null;
        private static readonly object instanceLock = new object();
        public static AccountDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AccountDAO();
                    }
                    return instance;
                }
            }
        }

        public Account GetAccountByID(int? accountID)
        {
            Account account = null;
            try
            {
                using var context = new DatabaseTestProjectContext();
                account = context.Accounts.SingleOrDefault(c => c.AccountId == accountID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return account;
        }

        public Account GetAccountByEmailAndPass(Account account)
        {
            Account acc = null;
            try
            {
                using var context = new DatabaseTestProjectContext();
                acc = context.Accounts.SingleOrDefault(c => c.Email == account.Email &&
                                                            c.Password == account.Password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return acc;
        }

        public void Update(Account account)
        {
            try
            {
                Account accountFind = GetAccountByID(account.AccountId);
                if (accountFind != null)
                {
                        using var context = new DatabaseTestProjectContext();
                        context.Accounts.Update(account);
                        context.SaveChanges();
                }
                else
                {
                    throw new Exception("The account does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNew(Account account)
        {
            try
            {
                Account accountFind = GetAccountByID(account.AccountId);
                if (accountFind == null)
                {
                    using var context = new DatabaseTestProjectContext();
                    context.Accounts.Add(account);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CheckReturnBook(Account account)
        {
            List<BooksBorrow> booksBorrows = null;
            try
            {
                Account accountFind = GetAccountByID(account.AccountId);
                if (accountFind != null)
                {
                    using var context = new DatabaseTestProjectContext();
                    booksBorrows = (from a in context.Accounts
                           join bb in context.BooksBorrows on a.AccountId equals bb.AccountId
                           where a.AccountId == account.AccountId && bb.DateReturn == null
                           select new BooksBorrow
                           {
                               DateBorrowed = bb.DateBorrowed,
                           }).ToList();
                    if(booksBorrows.Count != 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Account> GetAllAccounts()
        {
            List<Account> listAccount = null;
            try
            {
                using var context = new DatabaseTestProjectContext();
                listAccount = context.Accounts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listAccount;
        }

        public void DeleteAccount(int? id)
        {
            try
            {
                Account accountFind = GetAccountByID(id);
                if (accountFind != null)
                {
                    using var context = new DatabaseTestProjectContext();
                    context.Accounts.Remove(accountFind);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The account does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Account> SearchAccByNameOrEmailOrPhoneOrAddress(string? search)
        {
            List<Account> accounts = null;
            try
            {
                using var context = new DatabaseTestProjectContext();
                accounts = context.Accounts.Where(b =>
                (b.FirstName.Trim() + " " + b.LastName.Trim()) .Contains(search.Trim()) ||
                b.Email.Trim().Contains(search.Trim()) ||
                b.Phone.Trim().Contains(search.Trim()) ||
                b.Address.Trim().Contains(search.Trim())
            ).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return accounts;
        }

        public List<Account> GetAccountByRoleId(int? roleId)
        {
            List<Account> accounts = null;
            try
            {
                using var context = new DatabaseTestProjectContext();
                accounts = (from a in context.Accounts
                                  join r in context.Roles on a.RoleId equals r.RoleId
                                  where a.RoleId == roleId
                                  select new Account
                                  {
                                      AccountId = a.AccountId
                                  }).ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return accounts;
        }
    }
}
