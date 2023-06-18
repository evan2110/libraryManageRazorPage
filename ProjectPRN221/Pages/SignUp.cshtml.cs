using ManageBookLibrary.BusinessObject;
using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectPRN221.Pages
{
    public class SignUpModel : PageModel
    {
        IAccountRepository accountRepository = new AccountRepository();
        IRoleRepository roleRepository = new RoleRepository();

        public Account account { get; set; }
        public void OnGet()
        {
            List<Role> listRoles = roleRepository.GetRoles().Where(r => r.RoleName != "Admin").ToList();
            ViewData["listRoles"] = listRoles;
        }

        public void OnPost(Account account)
        {
            account.CreatedTime = DateTime.Now;
            account.Status = true;
            accountRepository.InsertAccount(account);
            List<Role> listRoles = roleRepository.GetRoles().Where(r => r.RoleName != "Admin").ToList();
            ViewData["listRoles"] = listRoles;
            ViewData["infor"] = "registersuss";
        }
    }
}
