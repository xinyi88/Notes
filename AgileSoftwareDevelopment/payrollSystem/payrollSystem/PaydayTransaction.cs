using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class PaydayTransaction : Transaction
    {
        private readonly DateTime payDate;
        private Dictionary<int, Paycheck> paychecks = 
            new Dictionary<int, Paycheck>();

        public PaydayTransaction(DateTime payDate, PayrollDatabase database)
            : base(database)
        {
            this.payDate = payDate;
        }

        public override void Execute()
        {
            var empIds = database.GetAllEmployeeIds();

            foreach (int empId in empIds)
            {
                Employee employee = database.GetEmployee(empId);
                if (employee.IsPayDate(payDate))
                {
                    DateTime startDate =
                        employee.GetPayPeriodStartDate(payDate);
                    Paycheck pc = new Paycheck(startDate, payDate);
                    paychecks[empId] = pc;
                    employee.Payday(pc);
                }
            }
        }

        public Paycheck GetPaycheck(int empId)
        {
            if (paychecks.ContainsKey(empId))
                return paychecks[empId];

            return null;
        }
    }

}