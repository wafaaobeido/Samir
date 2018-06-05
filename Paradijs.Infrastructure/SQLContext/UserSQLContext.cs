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

        //private string connectionstring()
        //{
        //    string _connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\388227\Desktop\Paradijs\Paradijs\App_Data\Paradij_DB.mdf;Integrated Security=True";
        //    return _connectionstring;
        //}

        public static string CS = ConfigurationManager.ConnectionStrings["LOCALDATABASE"].ConnectionString;

        #endregion

        #region methodes


        // Registration

        public bool IsEmailExists(User user)
        {
            SqlConnection con = new SqlConnection(CS);

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
            SqlConnection con = new SqlConnection(CS);
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
            SqlConnection con = new SqlConnection(CS);

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
            SqlConnection con = new SqlConnection(CS);

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
            SqlConnection con = new SqlConnection(CS);
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


        // Bijwerken .....
        public List<User> AllUsers()
        {

            var model = new List<User>();
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "AllUsers",
                    Parameters =
                    {
                new SqlParameter("@Email", "samirobeido76@gmail.com")
                    }
                };
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var u = new User();
                    u.Id = Convert.ToInt32(reader["Id"]);
                    u.FirstName = (string)reader["FirstName"];
                    u.LastName = (string)reader["LastName"];
                    u.Postcode = (string)reader["Postcode"];
                    u.Email = (string)reader["EmailID"];
                    u.Mobile = Convert.ToInt32(reader["Mobile"]);
                    u.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    u.Adress = (string)reader["Adress"];
                    u.City = (string)reader["City"];
                    u.IsEmailVerified = (bool)reader["IsEmailVerified"];

                    model.Add(u);
                }
            }
            return model;
        }
        public void DeleteUser(int id)
        {
            User user = new User();
            SqlConnection con = new SqlConnection(CS);
            con.Open(); string query = "Delete From [User] Where Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public void EditUser()
        {
            User user = new User();
            SqlConnection con = new SqlConnection(CS);
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

        public void SendMessage(Message Message)
        {
            SqlConnection conn = new SqlConnection(CS);
            conn.Open();
            SqlCommand cmd1 = new SqlCommand(
                @"INSERT INTO [Message] ( SenderID, RecipientID, ProductID, Subject, Body) VALUES (@senderid, @recipientid, @productid, @subject, @body)", conn);
            cmd1.Parameters.AddWithValue("@recipientid", Message.Recipient);
            cmd1.Parameters.AddWithValue("@senderid", Message.Sender);
            cmd1.Parameters.AddWithValue("@productid", Message.ProductID);
            cmd1.Parameters.AddWithValue("@subject", Message.Subject);
            cmd1.Parameters.AddWithValue("@body", Message.Body);
            cmd1.ExecuteNonQuery();
            conn.Close();
        }

        public List<ViewModelMessages> ViewAllMessages(User User)
        {
            List<ViewModelMessages> AllMessages = new List<ViewModelMessages>();
            SqlConnection conn = new SqlConnection(CS);
            conn.Open();

            SqlCommand cmd = new SqlCommand("ShowAllMessages", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@recipientid", User.Id));


            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    ViewModelMessages viewmodelmessage = new ViewModelMessages();
                    viewmodelmessage.NumberOfMessages = rdr.GetInt32(0);
                    viewmodelmessage.SenderID = rdr.GetInt32(1);
                    viewmodelmessage.Productid = rdr.GetInt32(2);
                    viewmodelmessage.RecipientID = rdr.GetInt32(3);
                    AllMessages.Add(viewmodelmessage);
                }
            }
            conn.Close();
            return AllMessages;
        }

        public List<Message> MessagesForOneProduct(User recipient, User sender, Product product)
        {
            List<Message> MessagesForOneProduct = new List<Message>();
            SqlConnection conn = new SqlConnection(CS);
            conn.Open();
            
            SqlCommand cmd = new SqlCommand("MessagesForOneProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@recipientid", recipient.Id));
            cmd.Parameters.Add(new SqlParameter("@senderid", sender.Id));
            cmd.Parameters.Add(new SqlParameter("@productid", product.Id));

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    Message Message = new Message();
                    Message.Recipient = rdr.GetInt32(0);
                    Message.Sender = rdr.GetInt32(1);
                    Message.ProductID = rdr.GetInt32(2);
                    Message.Subject = rdr.GetString(3);
                    Message.Body = rdr.GetString(4);
              
                    MessagesForOneProduct.Add(Message);
                }
            }
            conn.Close();
            return MessagesForOneProduct;
        }


        public List<ViewModelMessages> MessageIndex(int id)
        {
            List<ViewModelMessages> AllMessages = new List<ViewModelMessages>();
            ViewModelMessages viewmodelmessage = new ViewModelMessages();
            SqlConnection conn = new SqlConnection(CS);
            conn.Open();

            SqlCommand cmd = new SqlCommand("MessageIndex", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@userid", id));

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {

                    viewmodelmessage.NumberOfMessages = rdr.GetInt32(0);
                    viewmodelmessage.SenderID = rdr.GetInt32(1);
                    viewmodelmessage.Productid = rdr.GetInt32(2);
                    viewmodelmessage.RecipientID = rdr.GetInt32(3);
                    AllMessages.Add(viewmodelmessage);
                }
            }
            conn.Close();
            return AllMessages;
        }

        public List<ViewModelMessages> MessageSent(int id)
        {
            List<ViewModelMessages> AllMessages = new List<ViewModelMessages>();
            ViewModelMessages viewmodelmessage = new ViewModelMessages();
            SqlConnection conn = new SqlConnection(CS);
            conn.Open();

            SqlCommand cmd = new SqlCommand("MessageSent", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@userid", id));

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {

                    viewmodelmessage.NumberOfMessages = rdr.GetInt32(0);
                    viewmodelmessage.SenderID = rdr.GetInt32(1);
                    viewmodelmessage.Productid = rdr.GetInt32(2);
                    viewmodelmessage.RecipientID = rdr.GetInt32(3);
                    AllMessages.Add(viewmodelmessage);
                }
            }
            conn.Close();
            return AllMessages;


        }

        #endregion
    }
}
