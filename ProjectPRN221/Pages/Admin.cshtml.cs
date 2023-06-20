using ManageBookLibrary.BusinessObject;
using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PagedList;
using System.Data;
using static System.Reflection.Metadata.BlobBuilder;

namespace ProjectPRN221.Pages
{
    public class AdminModel : PageModel
    {
        IRoleRepository roleRepository = new RoleRepository();
        IAccountRepository accountRepository = new AccountRepository();
        IBookBorrowRepository borrowRepository = new BookBorrowRepository();
        IBookRepository bookRepository = new BookRepository();
        public IPagedList<Role> PagedRoles { get; set; }
        public IPagedList<Account> PagedAccounts { get; set; }

        public Role role { get; set; }
        public Account acc { get; set; }
        public void OnGet(string? mode, int? handler, int? id, string? search, int? idDelete)
        {
           
            if(mode == "deleteRole")
            {
                roleRepository.DeleteRole(idDelete);
                Response.Redirect("Admin?mode=role");
            }
            if (mode == "createRole")
            {
                ViewData["mode"] = "createRole";
            }
            if(mode == "editRole")
            {
                var role = roleRepository.GetRoleById(id);
                ViewData["mode"] = "createRole";
                ViewData["role"] = role;
            }
            if(mode == "role")
            {
                var roles = roleRepository.GetRoles();
                if (search != null)
                {
                    roles = roleRepository.SearchRoleByName(search.Trim());
                }
                var pageNumber = handler ?? 1;
                var pageSize = 10;
                PagedRoles = roles.ToPagedList(pageNumber, pageSize);
                var totalRoles = (PagedRoles.Count * PagedRoles.PageCount) / 10;
                ViewData["totalRoles"] = totalRoles;
                ViewData["roles"] = PagedRoles;
                ViewData["mode"] = "role";
            }
            if(mode == "acc")
            {
                var accs = accountRepository.GetAllAccounts();
                if (search != null)
                {
                    accs = accountRepository.SearchAccByNameOrEmailOrPhoneOrAddress(search.Trim());
                }
                var pageNumber = handler ?? 1;
                var pageSize = 10;
                PagedAccounts = accs.ToPagedList(pageNumber, pageSize);
                var totalAccs = (PagedAccounts.Count * PagedAccounts.PageCount) / 10;
                ViewData["totalAccs"] = totalAccs;
                ViewData["accs"] = PagedAccounts;
                ViewData["mode"] = "acc";
            }
            if(mode == "createAcc")
            {
                ViewData["mode"] = "createAcc";
            }
            if (mode == null || mode == "dashboard")
            {
                ViewData["mode"] = "dashboard";
            }

        }

        public void OnPost(Role? role, Account? acc)
        {
            if (role != null)
            {
                if (role.RoleId != 0)
                {
                    roleRepository.UpdateRole(role);
                }
                else
                {
                    roleRepository.InsertRole(role);
                }
                var roles = roleRepository.GetRoles();
                var pageNumber = 1;
                var pageSize = 10;
                PagedRoles = roles.ToPagedList(pageNumber, pageSize);
                var totalRoles = (PagedRoles.Count * PagedRoles.PageCount) / 10;
                ViewData["totalRoles"] = totalRoles;
                ViewData["roles"] = PagedRoles;
                ViewData["mode"] = "role";

                Response.Redirect("Admin?mode=role");

            }
            if (acc != null)
            {
                if (acc.RoleId != 0)
                {
                    accountRepository.UpdateAccount(acc);
                }
                else
                {
                    accountRepository.InsertAccount(acc);
                }
                var accs = accountRepository.GetAllAccounts();
                var pageNumber = 1;
                var pageSize = 10;
                PagedAccounts = accs.ToPagedList(pageNumber, pageSize);
                var totalAccs = (PagedAccounts.Count * PagedAccounts.PageCount) / 10;
                ViewData["totalAccs"] = totalAccs;
                ViewData["accs"] = PagedAccounts;
                ViewData["mode"] = "acc";

                Response.Redirect("Admin?mode=acc");

            }

        }
    }
}
