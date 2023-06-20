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
        public IPagedList<Role> PagedRoles { get; set; }
        public Role role { get; set; }
        public void OnGet(string? mode, int? handler, int? id, string? search)
        {
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
            if(mode == "createRole")
            {
                ViewData["mode"] = "createRole";
            }
            if(mode == "editRole")
            {
                var role = roleRepository.GetRoleById(id);
                ViewData["mode"] = "createRole";
                ViewData["role"] = role;
            }
            if (mode == null || mode == "dashboard")
            {
                ViewData["mode"] = "dashboard";
            }

        }

        public void OnPost(Role? role)
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

            }

        }
    }
}
