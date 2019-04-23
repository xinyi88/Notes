namespace ConsoleApp1
{
    public interface IAddEmployeeTransaction
    {
        PaymentClassification GetClassification();
        PaymentSchedule GetSchedule();

    }
}
