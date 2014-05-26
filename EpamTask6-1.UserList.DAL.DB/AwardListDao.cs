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

    public class AwardListDao : IAwardListDao
    {
        private static string connectionString;

        public AwardListDao()
        {
            connectionString = ConfigurationManager.connectionStrings["UsersAwardsDBConnection"].connectionString;

        }

        public bool AddAward(Award award)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("INSERT INTO dbo.Awards (Title) VALUES (@Title)", con);
                command.Parameters.Add(new SqlParameter("@Title", award.Title));    

                con.Open();
                var reader = command.ExecuteNonQuery();

                return reader > 0 ? true : false;

            }
        }

        public bool AddAwardToUser(System.Guid user_id, System.Guid award_id)
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

        public Award GetAward(System.Guid id)
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

        public bool DeleteUserAwards(User user)
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

        public System.Collections.Generic.IEnumerable<Award> GetUserAwards(User user)
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

        public System.Collections.Generic.IEnumerable<Award> GetAllAwards()
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

        public System.Collections.Generic.IEnumerable<UsersAward> GetAllUserAwards()
        {
            throw new System.NotImplementedException();  
        }

        public bool SetAllAwards(System.Collections.Generic.IEnumerable<Award> awards)
        {
            throw new System.NotImplementedException();
        }

        public bool SetAllUserAwards(System.Collections.Generic.IEnumerable<UsersAward> _usersAndAwardsList)
        {
            throw new System.NotImplementedException();
        }

        public bool SetAwardImage(System.Guid id)
        {
            try
            {
                this.RemoveAwardImage(id);

                using (var con = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("INSERT INTO dbo.AwardImage (ID, Image) VALUES (@ID, @Image)", con);
                    command.Parameters.Add(new SqlParameter("@ID", id));
                    command.Parameters.Add(new SqlParameter("@Image", id.ToString()));

                    con.Open();
                    var reader = command.ExecuteNonQuery();

                    return reader > 0 ? true : false;

                }
            }
            catch
            {
                return false;
            }
        }


        public bool GetAwardImage(System.Guid id)
        {
            if (this.GetAward(id) == null)
            {
                return false;
            }

            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("SELECT TOP 1 ID, image FROM dbo.AwardImage WHERE dbo.AwardImage.ID = @id", con);
                    command.Parameters.Add(new SqlParameter("@id", id));

                    con.Open();
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveAwardImage(System.Guid id)
        {
            if (this.GetAward(id) == null)
            {
                return false;
            }

            try
            {
                if (this.GetAwardImage(id))
                {
                    using (var con = new SqlConnection(connectionString))
                    {
                        var command = new SqlCommand("DELETE FROM dbo.AwardImage WHERE dbo.AwardImage.ID = @ID", con);
                        command.Parameters.Add(new SqlParameter("@ID", id));

                        con.Open();
                        var reader = command.ExecuteNonQuery();

                        return reader > 0 ? true : false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
