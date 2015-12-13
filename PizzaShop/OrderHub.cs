using System;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace PizzaShop
{
    public class OrderHub : Hub
    {
        public void Send(string name, string message)
        {
            // Update clients.
            Clients.All.broadcastMessage(name, message);
        }
    }
}