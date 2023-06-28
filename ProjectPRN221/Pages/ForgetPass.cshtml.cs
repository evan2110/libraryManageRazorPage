using ManageBookLibrary.BusinessObject;
using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectPRN221.Pages
{
    public class ForgetPassModel : PageModel
    {
        IAccountRepository accountRepository = new AccountRepository();
        public string Phone { get; set; }
        public void OnGet()
        {
        }

        public void OnPost(string Phone) {
            var acc = accountRepository.GetAllAccounts().FirstOrDefault(e => e.Phone == Phone);
            if (acc == null)
            {
                ViewData["checkPhone"] = "Error";
            }
            else
            {
                ViewData["phone"] = Phone;
            }
        }
    }
}
