using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN221.BusinessObject3;

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
            if(acc != null && acc.Status == true)
            {
                if(acc.RoleId == 1)
                {
                    HttpContext.Session.SetString("UserRole", "Admin");
                    HttpContext.Session.SetString("Email", acc.Email);
                    HttpContext.Session.SetString("Password", acc.Password);

                    Response.Redirect("/Admin");
                }
                else if(acc.RoleId == 2)
                {
                    HttpContext.Session.SetString("UserRole", "Manager");
                    HttpContext.Session.SetString("Email", acc.Email);
                    HttpContext.Session.SetString("Password", acc.Password);

                    Response.Redirect("/Student");
                }
                else if(acc.RoleId == 3)
                {
                    HttpContext.Session.SetString("UserRole", "Student");
                    HttpContext.Session.SetString("Email", acc.Email);
                    HttpContext.Session.SetString("Password", acc.Password);
                    bool check =accountRepository.CheckReturnBook(acc);
                    if (check)
                    {
                        HttpContext.Session.SetString("CheckReturnBook", "Yes");
                    }
                    else
                    {
                        HttpContext.Session.SetString("CheckReturnBook", "No");
                    }

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
