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
    public class ImageBD : IImage
    {
        #region Fields

        private string connectionstring()
        {
            string _connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\388227\Desktop\Paradijs\Paradijs\App_Data\Paradij_DB.mdf;Integrated Security=True";
            return _connectionstring;
        }

        #endregion

        #region methodes

        public Image AddImage(Image image)
        {
            Image newimage = image;

            SqlConnection con = new SqlConnection(connectionstring());
            con.Open();
            string query = "INSERT INTO Image(ImagePath, ImageTitle) Values ( @ImagePath, @ImageTitle )";
            SqlCommand addimage = new SqlCommand
            {
                Connection = con,
                CommandType = CommandType.Text,
                CommandText = query,
                Parameters =
                {
                    new SqlParameter("@ImagePath", image.ImagePath),
                    new SqlParameter("@ImageTitle", image.ImageTitle)
                }
            };


            newimage.Id = Convert.ToInt32(addimage.ExecuteScalar());
            con.Close();
            return newimage;


        }


        #endregion
    }
}
