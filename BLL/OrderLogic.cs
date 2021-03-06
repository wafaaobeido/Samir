﻿using DAL;
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
        public OrderRepository repo = new OrderRepository( new OrderSQLContext());

        public Order GetInfo(int clientid, int productid, int hostid)
        {
            Order order = new Order();
            order = repo.GetInfo(clientid, productid, hostid);
            return order;
        }
        public string AddOrder(int Klantid, int Verkoperid, int Productid, int quantity)
        {
            return repo.AddOrder(Klantid, Verkoperid, Productid, quantity);
        }
        public string KoppelTabelOrder(int Klantid, int Verkoperid, int Productid, int quantity)
        {
            return repo.KoppelTabelOrder(Klantid, Verkoperid, Productid, quantity);
        }
        public List<Order> ShowOrder(int id)
        {
            return repo.ShowOrder(id);
        }
        public void StandardMessage(int UserHostID, int UserRecipientID, string messages, string Subject, int ProductID)
        {
            repo.StandardMessage(UserHostID, UserRecipientID, messages, Subject, ProductID);
        }
        public List<Order> OrdersByUsers()
        {
            return repo.OrdersByUsers();
        }
        public List<Order> AllOrders()
        {
            return repo.AllOrders();
        }
    }
}
