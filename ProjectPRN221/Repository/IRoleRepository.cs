using ProjectPRN221.BusinessObject2;

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
        Role GetRoleByAccountId(int? accountId);
    }
}
