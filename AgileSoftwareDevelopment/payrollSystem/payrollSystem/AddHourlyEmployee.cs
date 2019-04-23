namespace ConsoleApp1
{
    public class AddHourlyEmployee : AddEmployeeTransaction
    {
        private readonly double hourlyRate;

        public AddHourlyEmployee(int id, string name, string address, double hourlyRate, PayrollDatabase database)
            : base(id, name, address, database)
        {
            this.hourlyRate = hourlyRate;

        }

        protected override
            PaymentClassification GetClassification()
        {
            return new HourlyClassification(hourlyRate);
        }

        protected override PaymentSchedule GetSchedule()
        {
            return new WeeklySchedule();
        }
    }
}
