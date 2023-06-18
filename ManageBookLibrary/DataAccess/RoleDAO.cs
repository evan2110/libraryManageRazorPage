using ManageBookLibrary.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
