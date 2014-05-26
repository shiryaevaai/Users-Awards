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

    public class RolesDao
    {
        private static string connectionString;

        public RolesDao()
        {
            connectionString = ConfigurationManager.connectionStrings["UsersAwardsDBConnection"].connectionString;
        }

        public bool AddAccount(AddAccount AddAccount)
        {
            throw new System.NotImplementedException();
        }

        public bool AddRoleToAccount(System.Guid AccountID, System.Guid RoleID)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("INSERT INTO dbo.UserAwards (UserID, AwardID) VALUES (@UserID, @AwardID)", con);
                command.Parameters.Add(new SqlParameter("@UserID", user_id));
                command.Parameters.Add(new SqlParameter("@AwardID", award_id));

                con.Open();
                var reader = command.ExecuteNonQuery();

                return reader > 0 ? true : false;

            }
        }
        
        public Account GetAccount(System.Guid id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT TOP 1 ID, Title FROM dbo.Awards WHERE dbo.Awards.ID = @id", con);
                command.Parameters.Add(new SqlParameter("@id", id));

                con.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Award()
                    {
                        ID = (System.Guid)reader["ID"],
                        Title = (string)reader["Title"],

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
            //using (var con = new SqlConnection(connectionString))
            //{
            //    var command = new SqlCommand("DELETE FROM dbo.UserAwards WHERE dbo.UserAwards.ID = @ID", con);
            //    command.Parameters.Add(new SqlParameter("@ID", id));

            //    con.Open();
            //    var reader = command.ExecuteNonQuery();

            //    return reader > 0 ? true : false;
            //}
            return true;
        }

        public System.Collections.Generic.IEnumerable<Role> GetAccountRoles(Account account)
        {
            using (var con = new SqlConnection(connectionString))
            {
                ///!!!!
                var command = new SqlCommand("SELECT ID, Title FROM dbo.Awards WHERE dbo.Awards.ID = (SELECT ID FROM dbo.UserAwards WHERE dbo.UserAwards.UserID = @UserID)", con);
                command.Parameters.Add(new SqlParameter("@UserID", user.ID));
                
                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new Award()
                    {
                        ID = (System.Guid)reader["ID"],
                        Title = (string)reader["Title"],
                    };
                }
            }  
        }

        public System.Collections.Generic.IEnumerable<Role> GetAllRoles()
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT ID, Title FROM dbo.Awards", con);
                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new Award()
                    {
                        ID = (System.Guid)reader["ID"],
                        Title = (string)reader["Title"],
                    };
                }
            }  
        }

        public System.Collections.Generic.IEnumerable<Account> GetAllAccounts()
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT ID, Title FROM dbo.Awards", con);
                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new Award()
                    {
                        ID = (System.Guid)reader["ID"],
                        Title = (string)reader["Title"],
                    };
                }
            }
        }

        
    }
}
