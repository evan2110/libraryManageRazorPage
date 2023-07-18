using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN221.BusinessObject2;

namespace ProjectPRN221.Pages
{
    public class AccountDetailModel : PageModel
    {
        IAccountRepository accountRepository = new AccountRepository();
        public Account account { get; set; }

        public void OnGet()
        {
            if(HttpContext.Session.GetString("UserRole") != null)
            {
                Account acc = accountRepository.GetAccountByEmailAndPass(new Account(HttpContext.Session.GetString("Email"), HttpContext.Session.GetString("Password")));
                ViewData["acc"] = acc;
            }
            else
            {
                Response.Redirect("Error");
            }

        }

        public void OnPost(Account account)
        {
            Account acc = accountRepository.GetAccountByEmailAndPass(new Account(HttpContext.Session.GetString("Email"), HttpContext.Session.GetString("Password")));

            int accountID = acc.AccountId;
            account.Phone = acc.Phone;
            if (accountRepository.GetAllAccounts().Where(e => e.Email != account.Email).FirstOrDefault(e => e.Email == acc.Email) != null)
            {
                ViewData["errorMail"] = "check";
            }
            else
            {
                account.AccountId = accountID;
                account.RoleId = acc.RoleId;

                account.Status = acc.Status;
                account.CreatedTime = acc.CreatedTime;
                account.UpdateTime = DateTime.Now;
                accountRepository.UpdateAccount(account);
                ViewData["infor"] = "updatesuss";
            }
            ViewData["acc"] = account;
        }

        
    }
}
