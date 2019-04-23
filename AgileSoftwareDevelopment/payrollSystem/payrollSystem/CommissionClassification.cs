namespace ConsoleApp1
{
    internal class CommissionClassification : PaymentClassification
    {
        private double baseRate;
        private double commissionRate;

        public CommissionClassification(double baseRate, double commissionRate)
        {
            this.baseRate = baseRate;
            this.commissionRate = commissionRate;
        }

        public override double CalculatePay(Paycheck paycheck)
        {
            throw new System.NotImplementedException();
        }
    }
}