using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Paycheck
    {
        private DateTime payDate;
        private readonly DateTime payPeriodStartDate;
        private double grossPay;
        private Dictionary<string, string> fields = new Dictionary<string, string>();
        private double deductions;
        private double netPay;

        public Paycheck(DateTime payPeriodStartDate, DateTime payDate)
        {
            this.payDate = payDate;
            this.payPeriodStartDate = payPeriodStartDate;
        }

        public DateTime PayDate
        {
            get { return payDate; }
        }

        public double GrossPay
        {
            get { return grossPay; }
            set { grossPay = value; }
        }  

        public double Deductions
        {
            get { return deductions; }
            set { deductions = value; }
        }

        public double NetPay
        {
            get { return netPay; }
            set { netPay = value; }
        }

        public DateTime PayPeriodEndDate
        {
            get { return payDate; }
        }

        public DateTime PayPeriodStartDate
        {
            get { return payPeriodStartDate; }
        }

        public void SetField(string field, string name)
        {
            fields.Add(field, name);
        }

        public string GetField(string fieldName)
        {
            return fields[fieldName];
        }
    }
}
