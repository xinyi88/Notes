using System;

namespace ConsoleApp1
{
    public class ServiceCharge
    {
        public DateTime Time { get; set; }
        public double Amount { get; set; }

        public ServiceCharge(DateTime time, double amount)
        {
            Time = time;
            Amount = amount;
        }

    }
}