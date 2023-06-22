using ManageBookLibrary.BusinessObject;
using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            account.AccountId = accountID;
            account.RoleId = acc.RoleId;
            account.Status = acc.Status;
            account.CreatedTime = acc.CreatedTime;
            account.UpdateTime = DateTime.Now;

            accountRepository.UpdateAccount(account);
            ViewData["acc"] = account;
            ViewData["infor"] = "updatesuss";
        }

        
    }
}
