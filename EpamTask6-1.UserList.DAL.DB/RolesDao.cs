namespace EpamTask6_1.UserList.DAL.DB
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EpamTask6_1.UserList.DAL.Abstract;
    using EpamTask6_1.UserList.Entities;
    using System.Configuration;
    using System.Data.SqlClient;

    public class RolesDao : IRolesDao
    {
        private static string connectionString;

        public RolesDao()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UsersAwardsDBConnection"].ConnectionString;
       
        }

        public bool AddAccount(Account account)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("INSERT INTO dbo.AppAccounts (ID, Login, Password) VALUES (@ID, @Login, @Password)", con);
                command.Parameters.Add(new SqlParameter("@ID", account.ID));
                command.Parameters.Add(new SqlParameter("@Login", account.Login));
                command.Parameters.Add(new SqlParameter("@Password", account.Password));

                con.Open();
                var reader = command.ExecuteNonQuery();

                return reader > 0 ? true : false;
            }
        }

        public bool AddRoleToAccount(System.Guid AccountID, System.Guid RoleID)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("INSERT INTO dbo.AppUserRoles (UserID, RoleID) VALUES (@UserID, @RoleID)", con);
                command.Parameters.Add(new SqlParameter("@UserID", AccountID));
                command.Parameters.Add(new SqlParameter("@RoleID", RoleID));

                con.Open();
                var reader = command.ExecuteNonQuery();

                return reader > 0 ? true : false;
            }
        }
        
        public Account GetAccount(System.Guid id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT TOP 1 [ID], [Login], [Password] FROM dbo.[AppAccounts] WHERE dbo.[AppAccounts].[ID] = @id", con);
                command.Parameters.Add(new SqlParameter("@id", id));

                con.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Account()
                    {
                        ID = (System.Guid)reader["ID"],
                        Login = (string)reader["Login"],
                        Password = (string)reader["Password"],
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        public bool DeleteAccount(Account account)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("DELETE FROM dbo.AppAccounts WHERE ID = @ID", con);
                command.Parameters.Add(new SqlParameter("@ID", account.ID));

                con.Open();
                var reader = command.ExecuteNonQuery();

                return reader > 0 ? true : false;
            }
        }

        public System.Collections.Generic.IEnumerable<Role> GetAccountRoles(Account account)
        {
            using (var con = new SqlConnection(connectionString))
            {
                ///!!!!
                var command = new SqlCommand("SELECT dbo.AppRoles.ID, dbo.AppRoles.RoleName "+
                    "FROM dbo.AppRoles INNER JOIN dbo.AppUserRoles "+
                    "ON dbo.AppRoles.ID = dbo.AppUserRoles.RoleID " +
                    "WHERE dbo.AppUserRoles.UserID = @UserID", con);
                command.Parameters.Add(new SqlParameter("@UserID", account.ID));
                
                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {                    
                    yield return new Role()
                    {
                        ID = (System.Guid)reader["ID"],
                        RoleName = (string)reader["RoleName"],
                    };
                }
            }  
        }

        public System.Collections.Generic.IEnumerable<Role> GetAllRoles()
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT ID, RoleName FROM dbo.AppRoles", con);
                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new Role()
                    {
                        ID = (System.Guid)reader["ID"],
                        RoleName = (string)reader["RoleName"],
                    };
                }
            }  
        }

        public System.Collections.Generic.IEnumerable<Account> GetAllAccounts()
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT ID, Login, Password FROM dbo.AppAccounts", con);
                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new Account()
                    {
                        ID = (System.Guid)reader["ID"],
                        Login = (string)reader["Login"],
                        Password = (string)reader["Password"],
                    };
                }
            }
        }        
    }
}
