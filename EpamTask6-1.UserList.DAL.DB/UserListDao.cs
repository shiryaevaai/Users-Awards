namespace EpamTask6_1.UserList.DAL.DB
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EpamTask6_1.UserList.DAL.Abstract;
    using EpamTask6_1.UserList.Entities;
    using System.Data.SqlClient; 

    public class UserListDao : IUserListDao
    {
        private static string connectionString;

        public UserListDao()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UsersAwardsDBConnection"].ConnectionString;
       
        }
        
        public bool AddUser(User user)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("INSERT INTO dbo.[Users] ([ID], [Name], [DateOfBirth]) VALUES (@ID, @Name, @DateOfBirth)", con);
                command.Parameters.Add(new SqlParameter("@ID", user.ID));
                command.Parameters.Add(new SqlParameter("@Name", user.Name));
                command.Parameters.Add(new SqlParameter("@DateOfBirth", user.DateOfBirth));

                con.Open();
                var reader = command.ExecuteNonQuery();
                return reader > 0 ? true : false;                
            }
        }

        public User GetUser(Guid id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT TOP 1 [ID], [Name], [DateOfBirth] FROM dbo.[Users] WHERE [ID] = @id", con);
                command.Parameters.Add(new SqlParameter("@id", id));

                con.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new User()
                    {
                        ID = (Guid)reader["ID"],
                        Name = (string)reader["Name"],
                        DateOfBirth = (DateTime)reader["DateOfBirth"],
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        public IEnumerable<User> GetAllUsers()
        { 
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT [ID], [Name], [DateOfBirth] FROM dbo.[Users]", con);
                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new User()
                    {
                        ID=(Guid)reader["ID"],
                        Name=(string)reader["Name"],
                        DateOfBirth=(DateTime)reader["DateOfBirth"],
                    };
                }
            }           
        }

        public IEnumerable<UserImage> GetAllImages()
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT [ID], [Image] FROM dbo.[UserImage]", con);
                con.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    yield return new UserImage()
                    {
                        UserID = (Guid)reader["ID"],
                        Image = (string)reader["Image"],
                    };
                }
            }  
        }

        public bool DeleteUser(Guid id)
        {
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("DELETE FROM dbo.[Users] WHERE [ID] = @ID", con);
                command.Parameters.Add(new SqlParameter("@ID", id));
    
                con.Open();
                var reader = command.ExecuteNonQuery();

                return reader > 0 ? true : false;
            }
        }

        public bool SetAllUsers(IEnumerable<User> users)
        {
            //! delete all
            using (var con = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("DELETE FROM dbo.Users", con);
                con.Open();
                var reader = command.ExecuteNonQuery();
            }

            try
            {

                foreach (var user in users)
                {
                    if (!this.AddUser(user))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }          

        public bool SetUserImage(Guid id)
        {
            // wrong id, handle exception
            if (this.GetUser(id) == null)
            {
                return false;
            }

            try
            {
                this.RemoveUserImage(id);

                using (var con = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("INSERT INTO dbo.UserImage (ID, Image) VALUES (@ID, @Image)", con);
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

        public bool GetUserImage(Guid id)
        {
            // wrong id, handle exception
            if (this.GetUser(id) == null)
            {
                return false;
            }

            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("SELECT TOP 1 [ID], [Image] FROM dbo.UserImage WHERE [ID] = @id", con);
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

        public bool RemoveUserImage(Guid id)
        {
            // wrong id, handle exception
            if (this.GetUser(id) == null)
            {
                return false;
            }

            try
            {
                if (this.GetUserImage(id))
                {
                    using (var con = new SqlConnection(connectionString))
                    {
                        var command = new SqlCommand("DELETE FROM dbo.UserImage WHERE [ID] = @ID", con);
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
