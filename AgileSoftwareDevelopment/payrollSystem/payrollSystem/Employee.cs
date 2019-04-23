using System;
using System.Text;

namespace ConsoleApp1
{

    public class Employee
    {
        public readonly int EmpId;
        public string Name;
        public readonly string Address;
        public PaymentClassification Classification { get; set; }
        public PaymentSchedule Schedule { get; set; }
        public PaymentMethod Method { get; set; }

        private Affiliation affiliation = new NoAffiliation();

        public Employee(int empid, string name, string address)
        {
            this.EmpId = empid;
            this.Name = name;
            this.Address = address;
        }

        public Affiliation Affiliation
        {
            get { return affiliation; }
            set { affiliation = value; }
        }

        public bool IsPayDate(DateTime date)
        {
            return Schedule.IsPayDate(date);
        }

        public DateTime GetPayPeriodStartDate(DateTime date)
        {
            return Schedule.GetPayPeriodStartDate(date);
        }

        public void Payday(Paycheck paycheck)
        {
            double grossPay = Classification.CalculatePay(paycheck);
            double deductions = affiliation.CalculateDeductions(paycheck);
            double netPay = grossPay - deductions;
            paycheck.GrossPay = grossPay;
            paycheck.Deductions = deductions;
            paycheck.NetPay = netPay;
            Method.Pay(paycheck);
        }

    }
}
