using ManageBookLibrary.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageBookLibrary.Repository
{
    public interface IRoleRepository
    {
        List<Role> GetRoles();
        List<Role> SearchRoleByName(string name);
        Role GetRoleById(int? roleId);
        void InsertRole(Role role);
        void DeleteRole(int? roleId);
        void UpdateRole(Role role);
    }
}
