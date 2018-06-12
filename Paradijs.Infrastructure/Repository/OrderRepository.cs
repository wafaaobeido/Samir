using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderRepository
    {
        public IOrder context;

        public OrderRepository(IOrder context)
        {
            this.context = context;
        }

        public Order GetInfo(int clientid, int productid, int hostid)
        {
            return context.GetInfo(clientid, productid, hostid);
        }
        public string AddOrder(int Klantid, int Verkoperid, int Productid, int quantity)
        {
            return context.AddOrder(Klantid, Verkoperid, Productid, quantity);
        }
        public string KoppelTabelOrder(int Klantid, int Verkoperid, int Productid, int quantity)
        {
            return context.KoppelTabelOrder(Klantid, Verkoperid, Productid, quantity);
        }
        public List<Order> ShowOrder(int id)
        {
            return context.ShowOrdersForUser(id);
        }
        public void StandardMessage(int UserHostID, int UserRecipientID, string messages, string Subject, int ProductID)
        {
            context.StandardMessage(UserHostID, UserRecipientID, messages, Subject, ProductID);
        }
        public List<Order> OrdersByUsers()
        {
            return context.SortOrdersByUsers();
        }
        public List<Order> AllOrders()
        {
            return context.AllOrders();
        }
    }
}
