namespace ConsoleApp1
{
    public class DeleteEmployeeTransaction : Transaction
    {
        private readonly int empId;

        public DeleteEmployeeTransaction(int empId, PayrollDatabase database) : base(database)
        {
            this.empId = empId;
        }

        public override void Execute()
        {
            database.DeleteEmployee(empId);
        }
    }
}
