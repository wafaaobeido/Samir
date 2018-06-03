using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public interface IOrder
    {
        Order GetInfo(int clientid, int productid, int hostid);
        void StandardMessage(int UserHostID, int UserRecipientID, string messages, string Subject, int ProductID);
        string AddOrder(int Klantid, int Verkoperid, int Productid);
        void DeleteOrder();
        void EditOrder();
    }
}