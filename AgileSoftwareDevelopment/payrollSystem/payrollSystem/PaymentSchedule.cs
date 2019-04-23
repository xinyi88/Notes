using System;

namespace ConsoleApp1
{
    public interface PaymentSchedule
    {
        bool IsPayDate(DateTime payDate);
        DateTime GetPayPeriodStartDate(DateTime date);
    }
}
