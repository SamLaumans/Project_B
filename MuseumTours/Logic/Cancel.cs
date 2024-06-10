using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Program
{
    public class Cancel
    {
        private static List<Customer> listOfCustomers = DataAccess.ReadJsonCustomers();
        public static void CancelAppointment(string customerCodeToCancel)
        {
            List<Tours> tours = DataAccess.LoadTours();
            Tours tourToUpdate = tours.FirstOrDefault(t => t.Customer_Codes.Any(c => c.CustomerCode == customerCodeToCancel));
            if (tourToUpdate != null)
            {
                Customer customerToRemove = tourToUpdate.Customer_Codes.FirstOrDefault(c => c.CustomerCode == customerCodeToCancel);
                if (customerToRemove != null)
                {
                    tourToUpdate.Customer_Codes.Remove(customerToRemove);
                    tourToUpdate.Spots++;
                    listOfCustomers.Add(customerToRemove);
                    DataAccess.WriteJsonToCustomers(listOfCustomers);
                    DataAccess.WriteJsonToTours(tours);
                    Program.World.WriteLine("Reservering succesvol gecanceled.");
                }
            }
            else
            {
                Program.World.WriteLine("U staat niet aangemeld voor een tour.");
            }
        }
    }
}

