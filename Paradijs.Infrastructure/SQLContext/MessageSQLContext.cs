using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class MessageSQLContext : IMessage
    {

        #region
        private CS_Databse cs_database = new CS_Databse();
        #endregion
        
        public void SendMessage(Message Message)
        {
            SqlConnection conn = new SqlConnection(cs_database.CS());
            conn.Open();
            SqlCommand cmd1 = new SqlCommand(
                @"INSERT INTO [Message] ( SenderID, RecipientID, ProductID, Subject, Body) VALUES (@senderid, @recipientid, @productid, @subject, @body)", conn);
            cmd1.Parameters.AddWithValue("@recipientid", Message.Recipient.Id);
            cmd1.Parameters.AddWithValue("@senderid", Message.Sender.Id);
            cmd1.Parameters.AddWithValue("@productid", Message.Product);
            cmd1.Parameters.AddWithValue("@subject", Message.Subject);
            cmd1.Parameters.AddWithValue("@body", Message.Body);
            cmd1.ExecuteNonQuery();
            conn.Close();
        }

        public List<ViewModelMessages> ViewAllMessages(User User)
        {
            List<ViewModelMessages> AllMessages = new List<ViewModelMessages>();
            SqlConnection conn = new SqlConnection(cs_database.CS());
            conn.Open();

            SqlCommand cmd = new SqlCommand("ShowAllMessages", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@recipientid", User.Id));


            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    ViewModelMessages viewmodelmessage = new ViewModelMessages();
                    //Message viewmodelmessage = new Message();
                    viewmodelmessage.NumberOfMessages = rdr.GetInt32(0);
                    viewmodelmessage.Sender.Id = rdr.GetInt32(1);
                    viewmodelmessage.Recipient.Id = rdr.GetInt32(2);
                    viewmodelmessage.Product.Id = rdr.GetInt32(3);
                    AllMessages.Add(viewmodelmessage);
                }
            }
            conn.Close();
            return AllMessages;
        }

        public List<Message> MessagesForOneProduct(User recipient, User sender, Product product)
        {
            List<Message> MessagesForOneProduct = new List<Message>();
            SqlConnection conn = new SqlConnection(cs_database.CS());
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
                    Message.Recipient.Id = rdr.GetInt32(0);
                    Message.Sender.Id = rdr.GetInt32(1);
                    Message.Product.Id = rdr.GetInt32(2);
                    Message.Subject = rdr.GetString(3);
                    Message.Body = rdr.GetString(4);

                    MessagesForOneProduct.Add(Message);
                }
            }
            conn.Close();
            return MessagesForOneProduct;
        }


        public List<ViewModelMessages> MessageInbox(int id)
        {
            List<ViewModelMessages> AllMessages = new List<ViewModelMessages>();
            ViewModelMessages viewmodelmessage = new ViewModelMessages();
            //Message viewmodelmessage = new Message();
            SqlConnection conn = new SqlConnection(cs_database.CS());
            conn.Open();

            SqlCommand cmd = new SqlCommand("MessageIndex", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@userid", id));

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {

                    viewmodelmessage.NumberOfMessages = rdr.GetInt32(0);
                    viewmodelmessage.Sender.Id = rdr.GetInt32(1);
                    viewmodelmessage.Product.Id = rdr.GetInt32(2);
                    viewmodelmessage.Recipient.Id = rdr.GetInt32(3);
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
            //Message viewmodelmessage = new Message();
            SqlConnection conn = new SqlConnection(cs_database.CS());
            conn.Open();

            SqlCommand cmd = new SqlCommand("MessageSent", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@userid", id));

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {

                    viewmodelmessage.NumberOfMessages = rdr.GetInt32(0);
                    viewmodelmessage.Sender.Id = rdr.GetInt32(1);
                    viewmodelmessage.Product.Id = rdr.GetInt32(2);
                    viewmodelmessage.Recipient.Id = rdr.GetInt32(3);
                    AllMessages.Add(viewmodelmessage);
                }
            }
            conn.Close();
            return AllMessages;


        }


    }
}
