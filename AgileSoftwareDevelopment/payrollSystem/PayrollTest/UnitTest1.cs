using ConsoleApp1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace PayrollTest
{
    [TestClass]
    public class UnitTest1
    {
        private PayrollDatabase database;

        [TestInitialize]
        public void SetUp()
        {
            database = new PayrollDatabase();
        }

        [TestMethod]
        public void TestAddSalariedEmployee()
        {
            var empId = 1;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Bob", "Home", 1000.00, database);

            t.Execute();

            Employee e = database.GetEmployee(empId);
            Assert.AreEqual(e.Name, "Bob"); ;

            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is SalariedClassification);
            SalariedClassification sc = pc as SalariedClassification;

            Assert.AreEqual(1000.00, sc.Salary, .001);
            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is MonthlySchedule);

            PaymentMethod pm = e.Method;
            Assert.IsTrue(pm is HoldMethod);
        }

        [TestMethod]
        public void TestDeleteEmployee()
        {
            var empId = 3;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Lance", "Home", 2500, 3.2, database);

            t.Execute();

            var e = database.GetEmployee(empId);
            Assert.AreEqual(e.Name, "Lance");

            var deleteEmployeeTransaction = new DeleteEmployeeTransaction(empId, database);
            deleteEmployeeTransaction.Execute();

            e = database.GetEmployee(empId);
            Assert.IsNull(e);
        }

        [TestMethod]
        public void TestTimeCardTransaction()
        {
            int empId = 31;
            var t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
            t.Execute();

            var today = DateTime.UtcNow;
            var tct = new TimeCardTransaction(today, 8.0, empId, database);
            tct.Execute();

            Employee e = database.GetEmployee(empId);
            Assert.IsNotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.IsTrue(pc is HourlyClassification);
            HourlyClassification hc = pc as HourlyClassification;

            TimeCard tc = hc.GetTimeCard(today);
            Assert.IsNotNull(tc);
            Assert.AreEqual(8.0, tc.Hours);
        }

        [TestMethod]
        public void TestAddServiceCharge()
        {

            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(
                empId, "Bill", "Home", 15.25, database);
            t.Execute();
            Employee e = database.GetEmployee(empId);
            Assert.IsNotNull(e);
            UnionAffiliation af = new UnionAffiliation();
            e.Affiliation = af;
            int memberId = 86; // Maxwell Smart
            database.AddUnionMember(memberId, e);
            ServiceChargeTransaction sct =
                new ServiceChargeTransaction(
                    memberId, new DateTime(2005, 8, 8), 12.95, database);
            sct.Execute();
            ServiceCharge sc =
                af.GetServiceCharge(new DateTime(2005, 8, 8));
            Assert.IsNotNull(sc);
            Assert.AreEqual(12.95, sc.Amount, .001);
        }

        [TestMethod]
        public void TestChangeNameTransaction()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
            t.Execute();
            ChangeNameTransaction cnt = new ChangeNameTransaction(empId, "Bob", database);
            cnt.Execute();
            Employee e = database.GetEmployee(empId);
            Assert.IsNotNull(e);
            Assert.AreEqual("Bob", e.Name);
        }

        [TestMethod]
        public void TestChangeHourlyTransaction()
        {
            int empId = 3;
            AddCommissionedEmployee t =
                new AddCommissionedEmployee(
                    empId, "Lance", "Home", 2500, 3.2, database);
            t.Execute();
            ChangeHourlyTransaction cht =
                new ChangeHourlyTransaction(empId, 27.52, database);
            cht.Execute();
            Employee e = database.GetEmployee(empId);
            Assert.IsNotNull(e);
            PaymentClassification pc = e.Classification;
            Assert.IsNotNull(pc);
            Assert.IsTrue(pc is HourlyClassification);
            HourlyClassification hc = pc as HourlyClassification;
            Assert.AreEqual(27.52, hc.HourlyRate, .001);
            PaymentSchedule ps = e.Schedule;
            Assert.IsTrue(ps is WeeklySchedule);
        }

        [TestMethod]
        public void TestChangeMemberTransaction()
        {
            int empId = 9;
            AddHourlyEmployee t =
                new AddHourlyEmployee(empId, "Bill", "Home", 15.25, database);
            t.Execute();
            int memberId = 7743;
            ChangeMemberTransaction cmt =
                new ChangeMemberTransaction(empId, memberId, 99.42, database);
            cmt.Execute();
            Employee e = database.GetEmployee(empId);
            Assert.IsNotNull(e);
            Affiliation affiliation = e.Affiliation;
            Assert.IsNotNull(affiliation);
            Assert.IsTrue(affiliation is UnionAffiliation);
            UnionAffiliation uf = affiliation as UnionAffiliation;
            Assert.AreEqual(99.42, uf.Dues, .001);
            Employee member = database.GetUnionMember(memberId);
            Assert.IsNotNull(member);
            Assert.AreEqual(e, member);
        }

        [TestMethod]
        public void TestPaySingleSalariedEmployee()
        {
            int empId = 1;
            AddSalariedEmployee t = new AddSalariedEmployee(
                empId, "Bob", "Home", 1000.00, database);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 30);
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();
            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayDate);
            Assert.AreEqual(1000.00, pc.GrossPay, .001);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(0.0, pc.Deductions, .001);
            Assert.AreEqual(1000.00, pc.NetPay, .001);
        }

        [TestMethod]
        public void TestPayingSingleHourlyEmployeeNoTimeCards()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(
                empId, "Bill", "Home", 15.25, database);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 9); // Friday
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, 0.0);
        }

        [TestMethod]
        public void TestServiceChargesSpanningMultiplePayPeriods()
        {
            int empId = 1;
            AddHourlyEmployee t = new AddHourlyEmployee(
                empId, "Bill", "Home", 15.24, database);
            t.Execute();
            int memberId = 7734;
            ChangeMemberTransaction cmt =
                new ChangeMemberTransaction(empId, memberId, 9.42, database);
            cmt.Execute();
            DateTime payDate = new DateTime(2001, 11, 9);
            DateTime earlyDate =
                new DateTime(2001, 11, 2); // previous Friday
            DateTime lateDate =
                new DateTime(2001, 11, 16); // next Friday
            ServiceChargeTransaction sct =
                new ServiceChargeTransaction(memberId, payDate, 19.42, database);
            sct.Execute();
            ServiceChargeTransaction sctEarly =
                new ServiceChargeTransaction(memberId, earlyDate, 100.00, database);
            sctEarly.Execute();
            ServiceChargeTransaction sctLate =
                new ServiceChargeTransaction(memberId, lateDate, 200.00, database);
            sctLate.Execute();
            TimeCardTransaction tct =
                new TimeCardTransaction(payDate, 8.0, empId, database);
            tct.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();
            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayPeriodEndDate);
            Assert.AreEqual(8 * 15.24, pc.GrossPay, .001);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(9.42 + 19.42, pc.Deductions, .001);
            Assert.AreEqual((8 * 15.24) - (9.42 + 19.42), pc.NetPay, .001);
        }
        private void ValidatePaycheck(PaydayTransaction pt,
                                      int empid, DateTime payDate, double pay)
        {
            Paycheck pc = pt.GetPaycheck(empid);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayDate);
            Assert.AreEqual(pay, pc.GrossPay, .001);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(0.0, pc.Deductions, .001);
            Assert.AreEqual(pay, pc.NetPay, .001);
        }

        [TestMethod]
        public void TestPaySingleHourlyEmployeeOnWrongDate()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(
                empId, "Bill", "Home", 15.25, database);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 8); // Thursday

            TimeCardTransaction tc =
                new TimeCardTransaction(payDate, 9.0, empId, database);
            tc.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNull(pc);
        }

        [TestMethod]
        public void TestPaySingleHourlyEmployeeTwoTimeCards()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(
                empId, "Bill", "Home", 15.25, database);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 9); // Friday

            TimeCardTransaction tc =
                new TimeCardTransaction(payDate, 2.0, empId, database);
            tc.Execute();
            TimeCardTransaction tc2 =
                new TimeCardTransaction(payDate.AddDays(-1), 5.0, empId, database);
            tc2.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, 7 * 15.25);
        }

        [TestMethod]
        public void
            TestPaySingleHourlyEmployeeWithTimeCardsSpanningTwoPayPeriods()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(
                empId, "Bill", "Home", 15.25, database);
            t.Execute();
            DateTime payDate = new DateTime(2001, 11, 9); // Friday
            DateTime dateInPreviousPayPeriod =
                new DateTime(2001, 10, 30);

            TimeCardTransaction tc =
                new TimeCardTransaction(payDate, 2.0, empId, database);
            tc.Execute();
            TimeCardTransaction tc2 = new TimeCardTransaction(
                dateInPreviousPayPeriod, 5.0, empId, database);
            tc2.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();
            ValidatePaycheck(pt, empId, payDate, 2 * 15.25);
        }

        [TestMethod]
        public void TestSalariedUnionMemberDues()
        {
            int empId = 1;
            AddSalariedEmployee t = new AddSalariedEmployee(
                empId, "Bob", "Home", 1000.00, database);
            t.Execute();
            int memberId = 7734;
            ChangeMemberTransaction cmt =
                new ChangeMemberTransaction(empId, memberId, 9.42, database);
            cmt.Execute();
            DateTime payDate = new DateTime(2001, 11, 30);
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();
            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayDate);
            Assert.AreEqual(1000.0, pc.GrossPay, .001);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(47.1, pc.Deductions, .001);
            Assert.AreEqual(1000.0 - 47.1, pc.NetPay, .001);
        }

        [TestMethod]
        public void TestHourlyUnionMemberServiceCharge()
        {
            int empId = 1;
            AddHourlyEmployee t = new AddHourlyEmployee(
                empId, "Bill", "Home", 15.24, database);
            t.Execute();
            int memberId = 7734;
            ChangeMemberTransaction cmt =
                new ChangeMemberTransaction(empId, memberId, 9.42, database);
            cmt.Execute();
            DateTime payDate = new DateTime(2001, 11, 9);
            ServiceChargeTransaction sct =
                new ServiceChargeTransaction(memberId, payDate, 19.42, database);
            sct.Execute();
            TimeCardTransaction tct =
                new TimeCardTransaction(payDate, 8.0, empId, database);
            tct.Execute();
            PaydayTransaction pt = new PaydayTransaction(payDate, database);
            pt.Execute();
            Paycheck pc = pt.GetPaycheck(empId);
            Assert.IsNotNull(pc);
            Assert.AreEqual(payDate, pc.PayPeriodEndDate);
            Assert.AreEqual(8 * 15.24, pc.GrossPay, .001);
            Assert.AreEqual("Hold", pc.GetField("Disposition"));
            Assert.AreEqual(9.42 + 19.42, pc.Deductions, .001);
            Assert.AreEqual((8 * 15.24) - (9.42 + 19.42), pc.NetPay, .001);
        }
    }
}
