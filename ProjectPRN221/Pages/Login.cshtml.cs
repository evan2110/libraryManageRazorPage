using Azure;
using Azure.Core;
using ManageBookLibrary.BusinessObject;
using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectPRN221.Pages
{
    public class LoginModel : PageModel
    {
        IAccountRepository accountRepository = new AccountRepository();
        public Account account {get; set;} 
        public void OnGet()
        {
            if (HttpContext.Session.GetString("UserRole") != null)
            {
                HttpContext.Session.Clear();
            }
        }

        public void OnPost(Account account) {
            string email = account.Email;
            string password = account.Password;
            

            var acc = accountRepository.GetAccountByEmailAndPass(new Account(email, password));
            if(acc != null)
            {
                if(acc.RoleId == 1)
                {
                    Response.Redirect("/Admin");
                }
                else if(acc.RoleId == 2)
                {
                    Response.Redirect("/Manager");
                }
                else if(acc.RoleId == 3)
                {
                    HttpContext.Session.SetString("UserRole", "Student");
                    HttpContext.Session.SetString("Email", email);

                    Response.Redirect("/Student");
                }
            }
            else
            {
                Response.Redirect("/Error");
            }
        }
    }
}
