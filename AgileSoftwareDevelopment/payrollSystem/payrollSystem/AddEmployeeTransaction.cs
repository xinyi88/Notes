namespace ConsoleApp1
{
    public abstract class AddEmployeeTransaction : Transaction
    {
        public int EmpId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public AddEmployeeTransaction(int empId, string name, string address, PayrollDatabase database): base(database)
        {
            this.EmpId = empId;
            this.Name = name;
            this.Address = address;
        }

        protected abstract PaymentClassification GetClassification();
        protected abstract PaymentSchedule GetSchedule();

        public override void Execute()
        {
            PaymentClassification pc = GetClassification();
            PaymentSchedule ps = GetSchedule();
            PaymentMethod pm = new HoldMethod();
            Employee e = new Employee(EmpId, Name, Address);
            e.Classification = pc;
            e.Schedule = ps;
            e.Method = pm;
            database.AddEmployee(e);
        }


    }
}
