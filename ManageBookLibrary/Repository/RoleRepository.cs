using ManageBookLibrary.BusinessObject;
using ManageBookLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageBookLibrary.Repository
{
    public class RoleRepository : IRoleRepository
    {
        public Role GetRoleById(int? roleId) => RoleDAO.Instance.GetRoleById(roleId);

        public List<Role> GetRoles() => RoleDAO.Instance.GetRoleList();

        public List<Role> SearchRoleByName(string name) => RoleDAO.Instance.SearchRoleByName(name);
    }
}
