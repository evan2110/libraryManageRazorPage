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
        public void DeleteRole(int? roleId) => RoleDAO.Instance.DeleteRole(roleId);

        public Role GetRoleByAccountId(int? accountId) => RoleDAO.Instance.GetRoleByAccountId(accountId);

        public Role GetRoleById(int? roleId) => RoleDAO.Instance.GetRoleById(roleId);

        public List<Role> GetRoles() => RoleDAO.Instance.GetRoleList();

        public void InsertRole(Role role) => RoleDAO.Instance.InsertRole(role);

        public List<Role> SearchRoleByName(string name) => RoleDAO.Instance.SearchRoleByName(name);

        public void UpdateRole(Role role) => RoleDAO.Instance.UpdateRole(role);
    }
}
