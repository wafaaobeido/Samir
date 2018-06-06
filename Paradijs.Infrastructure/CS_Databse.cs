using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class CS_Databse
    {
        public string CS()
        {
            return ConfigurationManager.ConnectionStrings["LOCALDATABASE"].ConnectionString;
        }
    }
}
