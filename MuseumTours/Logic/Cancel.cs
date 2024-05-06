using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Program
{
    public class Cancel
    {
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
                    DataAccess.SaveTours(tours);
                    Console.WriteLine("Reservering succesvol gecanceled.");
                }
            }
            else
            {
                Console.WriteLine("U staat niet aangemeld voor een tour.");
            }
        }
    }
}

