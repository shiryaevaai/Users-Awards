namespace EpamTask6_1.UserList.DAL.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EpamTask6_1.UserList.Entities;

    public interface IRolesDao
    {
        bool AddAccount(Account account);

        bool AddRoleToAccount(System.Guid AccountID, System.Guid RoleID);
                
        Account GetAccount(System.Guid id);        

        bool DeleteAccount(Account account);
        
        System.Collections.Generic.IEnumerable<Role> GetAccountRoles(Account account);
        
        System.Collections.Generic.IEnumerable<Role> GetAllRoles();
        
        System.Collections.Generic.IEnumerable<Account> GetAllAccounts();
    }
}
