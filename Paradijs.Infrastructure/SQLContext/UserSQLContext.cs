using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserSQLContext : IUser
    {
        #region Fields

        private CS_Databse cs_database = new CS_Databse();

        #endregion

        #region methodes


        // Registration

        public bool IsEmailExists(User user)
        {
            SqlConnection con = new SqlConnection(cs_database.CS());

            try
            {
                con.Open();

                string query = "SELECT EmailID FROM [User] Where EmailID = @EmailID";
                SqlCommand checkEmail = new SqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.Text,
                    CommandText = query,
                    Parameters =
                    {
                        new SqlParameter("@EmailID", user.Email)
                    }
                };

                int check = Convert.ToInt32(checkEmail.ExecuteScalar());

                return check > 0;
            }
            finally
            {
                con.Close();
            }
        }
        public User AddUser(User user)
        {
            var newuser = new User();
            SqlConnection con = new SqlConnection(cs_database.CS());
            con.Open();
            string query = "INSERT INTO [User](FirstName, LastName, DateOfBirth, Adress, Postcode, City, EmailID, Mobile, Password, ConfirmPassword, IsEmailVerified, ActivationCode)" +
             " VALUES(@FirstName, @LastName, @DateOfBirth, @Adress, @Postcode, @City, @EmailID, @Mobile,  @Password, @ConfirmPassword, @IsEmailVerified, @ActivationCode)";

            SqlCommand cmd = new SqlCommand
            {
                Connection = con,
                CommandType = CommandType.Text,
                CommandText = query,
                Parameters = {
            new SqlParameter("@FirstName", user.FirstName),
            new SqlParameter("@LastName", user.LastName),
            new SqlParameter("@DateOfBirth", user.DateOfBirth),
            new SqlParameter("@Adress", user.Adress),
            new SqlParameter("@Postcode", user.Postcode),
            new SqlParameter("@City", user.City),
            new SqlParameter("@EmailID", user.Email),
            new SqlParameter("@Mobile", user.Mobile),
            new SqlParameter("@Password", user.Password),
            new SqlParameter("@ConfirmPassword", user.ConfirmPassword),
            new SqlParameter("@IsEmailVerified", user.IsEmailVerified),
            new SqlParameter("@ActivationCode", user.ActivationCode)
                             }

            };

            cmd.ExecuteNonQuery();
            newuser = user;
            con.Close();
            return newuser;
        }
        public bool IsActivationCodeExists(User user)
        {
            SqlConnection con = new SqlConnection(cs_database.CS());

            try
            {
                con.Open();

                string query = "SELECT ActivationCode FROM [User] Where ActivationCode = @ActivationCode";
                SqlCommand checkCode = new SqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.Text,
                    CommandText = query,
                    Parameters =
                    {
                        new SqlParameter("@ActivationCode", user.ActivationCode)
                    }
                };

                var check = checkCode.ExecuteScalar();

                return check != null;
            }
            finally
            {
                con.Close();
            }


        }
        public bool IsValidation(User user)
        {
            SqlConnection con = new SqlConnection(cs_database.CS());

            try
            {
                con.Open();

                string query = "Update [User] Set IsEmailVerified = @IsEmailVerified " +
                    " where ActivationCode = @ActivationCode";
                SqlCommand isvalidate = new SqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.Text,
                    CommandText = query,
                    Parameters =
                    {
                        new SqlParameter("@IsEmailVerified", user.IsEmailVerified),
                        new SqlParameter("@ActivationCode", user.ActivationCode)
                    }
                };

                bool check = Convert.ToBoolean(isvalidate.ExecuteNonQuery());
                return check = true;
            }
            finally
            {
                con.Close();
            }
        }

        // LogIn
        public User LogIn(User user)
        {
            SqlConnection con = new SqlConnection(cs_database.CS());
            try
            {
                con.Open();
                SqlCommand Logincmd = new SqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "usplogin ",
                    Parameters =
                    {
                        new SqlParameter("@EmailID", user.Email),
                        new SqlParameter("@Password", Utils.Hash(user.Password))

                    }
                };
                var model = new List<User>();
                SqlDataReader rdr = Logincmd.ExecuteReader();
                while (rdr.Read())
                {
                    user = Utils.UserFromReader(rdr);
                }

                //int x = Convert.ToInt32(Logincmd.ExecuteScalar());
                //user.Id = x;

                return user;
            }
            finally
            {
                con.Close();
            }

        }
        public bool Checkaccount(User user)
        {
            SqlConnection con = new SqlConnection(cs_database.CS());

            try
            {
                con.Open();

                string query = "SELECT * FROM [User] Where EmailID = @EmailID And Password = @password";
                SqlCommand checkEmail = new SqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.Text,
                    CommandText = query,
                    Parameters =
                    {
                        new SqlParameter("@EmailID", user.Email),
                        new SqlParameter("@Password", Utils.Hash(user.Password))

                    }
                };

                int check = Convert.ToInt32(checkEmail.ExecuteScalar());

                return check > 0;
            }
            finally
            {
                con.Close();
            }
        }

        // Bijwerken .....
        public List<User> AllUsers()
        {

            var model = new List<User>();
            using (SqlConnection con = new SqlConnection(cs_database.CS()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "AllUsers"
                   
                };
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Order o = new Order();
                    var u = new User(o);
                    o.Id = reader.GetInt32(0);
                    u.FirstName = reader.GetString(1);
                    u.LastName = reader.GetString(2);
                    u.Mobile = reader.GetInt32(3);
                    u.DateOfBirth = reader.GetDateTime(4);
                    u.Email = reader.GetString(5);
                    u.Adress = reader.GetString(6);
                    u.Postcode = reader.GetString(7);
                    u.City = reader.GetString(8);
                   // o.User.Id = reader.GetInt32(9);
                   if(reader["UserId"] != DBNull.Value) o.User.Id =  reader.GetInt32(9);

                    model.Add(u);
                }
            }
            return model;
        }
        public void DeleteUser(int id)
        {
            User user = new User();
            SqlConnection con = new SqlConnection(cs_database.CS());
            con.Open(); string query = "Delete From [User] Where Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public void EditUser()
        {
            User user = new User();
            SqlConnection con = new SqlConnection(cs_database.CS());
            con.Open(); string query = "Update User Set (FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, Adress =  @Adress, Postcode = @Postcode, City = @City, Email = @Email, Mobile = @Mobile,  Password = @Password) Where Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", user.Id);
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LasttName", user.LastName);
            cmd.Parameters.AddWithValue("@DatOfBirth", user.DateOfBirth);
            cmd.Parameters.AddWithValue("@Adress", user.Adress);
            cmd.Parameters.AddWithValue("@Postcode", user.Postcode);
            cmd.Parameters.AddWithValue("@City", user.City);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Mobile", user.Mobile);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.ExecuteNonQuery();
            con.Close();
        }


        #endregion
    }
}
