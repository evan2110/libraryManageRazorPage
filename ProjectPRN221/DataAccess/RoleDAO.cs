using ProjectPRN221.BusinessObject2;

namespace ManageBookLibrary.DataAccess
{
    public class RoleDAO
    {
        private static RoleDAO instance = null;
        private static readonly object instanceLock = new object();
        public static RoleDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoleDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Role> GetRoleList()
        {
            List<Role> listRole = null;
            try
            {
                using var context = new DatabaseTestProjectContext();
                listRole = context.Roles.Select(b => new Role
                {
                    RoleId = b.RoleId,
                    RoleName = b.RoleName,
                })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listRole;
        }

        public List<Role> SearchRoleByName(string name)
        {
            List<Role> roles = null;
            try
            {
                using var context = new DatabaseTestProjectContext();
                roles = context.Roles.Where(b =>
                b.RoleName.Trim().Contains(name.Trim())
            ).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return roles;
        }

        public Role GetRoleById(int? id)
        {
            Role role = null;
            try
            {
                using var context = new DatabaseTestProjectContext();
                role = context.Roles.SingleOrDefault(c => c.RoleId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return role;
        }

        public void InsertRole(Role role)
        {
            try
            {
                Role roleFind = GetRoleById(role.RoleId);
                if (roleFind == null)
                {
                        using var context = new DatabaseTestProjectContext();
                        context.Roles.Add(role);
                        context.SaveChanges();
                }
                else
                {
                    throw new Exception("The role is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateRole(Role role)
        {
            try
            {
                Role roleFind = GetRoleById(role.RoleId);
                if (roleFind != null)
                {

                    using var context = new DatabaseTestProjectContext();
                    context.Roles.Update(role);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The role does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteRole(int? roleID)
        {
            try
            {
                Role roleFind = GetRoleById(roleID);
                if (roleFind != null)
                {
                    using var context = new DatabaseTestProjectContext();
                    context.Roles.Remove(roleFind);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The role does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Role GetRoleByAccountId(int? accountId)
        {
            Role role = null;
            try
            {
                using var context = new DatabaseTestProjectContext();
                role = ((Role?)(from a in context.Accounts
                        join r in context.Roles on a.RoleId equals r.RoleId
                        where a.AccountId == accountId
                        select new Role
                        {
                            RoleId = r.RoleId,
                            RoleName = r.RoleName
                        }));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return role;
        }
    }
}
