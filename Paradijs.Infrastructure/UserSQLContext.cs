using Models;
using System;
using System.Collections.Generic;
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

        private string connectionstring()
        {
            string _connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\388227\Desktop\Paradijs\Paradijs\App_Data\Paradij_DB.mdf;Integrated Security=True";
            return _connectionstring;
        }

        #endregion

        #region methodes


        // Registration

        public bool IsEmailExists(User user)
        {
            SqlConnection con = new SqlConnection(connectionstring());

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
            SqlConnection con = new SqlConnection(connectionstring());
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
            SqlConnection con = new SqlConnection(connectionstring());

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
            SqlConnection con = new SqlConnection(connectionstring());

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
            SqlConnection con = new SqlConnection(connectionstring());
            User newuser = new User();
            try
            {
                con.Open();
                //string query = "SELECT FirstName FROM [User] Where EmailID = @EmailID AND Password = @Password";
                SqlCommand Logincmd = new SqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "usplogin ",
                    Parameters =
                    {
                        new SqlParameter("@EmailID", user.Email),
                        new SqlParameter("@Password", Crypto.Hash(user.Password))
                       
                    }
                };
                var model = new List<User>();
                SqlDataReader rdr = Logincmd.ExecuteReader();
                while (rdr.Read())
                {
                    var user1 = new User();
                    user1.Email = (string)rdr["EmailID"];
                    user1.IsEmailVerified = (bool)rdr["IsEmailVerified"];

                    user = user1;
                }
                //newuser = user;
                return user;


            }
            finally
            {
                con.Close();
            }

        }


        // Bijwerken .....

        public void DeleteUser()
        {
            User user = new User();
            SqlConnection con = new SqlConnection(connectionstring());
            con.Open(); string query = "Delete From User Where Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", user.Id);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public void EditUser()
        {
            User user = new User();
            SqlConnection con = new SqlConnection(connectionstring());
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
