using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrderLogic
    {
        public IOrder repo = new OrderSQLContext();

        public Order GetInfo(int clientid, int productid, int hostid)
        {
            Order order = new Order();
            order = repo.GetInfo(clientid, productid, hostid);
            return order;
        }
        public string AddOrder(int Klantid, int Verkoperid, int Productid)
        {
            return repo.AddOrder(Klantid, Verkoperid, Productid);
        }
        public void StandardMessage(int UserHostID, int UserRecipientID, string messages, string Subject, int ProductID)
        {
            repo.StandardMessage(UserHostID, UserRecipientID, messages, Subject, ProductID);
        }
    }
}
