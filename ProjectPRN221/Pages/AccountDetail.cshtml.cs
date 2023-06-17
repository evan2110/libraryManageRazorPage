using ManageBookLibrary.BusinessObject;
using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectPRN221.Pages
{
    public class AccountDetailModel : PageModel
    {
        IAccountRepository accountRepository = new AccountRepository();
        public void OnGet()
        {
            var acc = accountRepository.GetAccountByEmailAndPass(new Account(HttpContext.Session.GetString("Email"), HttpContext.Session.GetString("Password")));
            ViewData["acc"] = acc;
        }
    }
}
