using System;

namespace ConsoleApp1
{
    public class ServiceChargeTransaction : Transaction
    {
        private int itsMemberId;
        private DateTime itsDate;
        private double itsCharge;
       

        public ServiceChargeTransaction(int memberId, DateTime dateTime, double charge, PayrollDatabase database) : base(database)
        {
            this.itsMemberId = memberId;
            this.itsDate = dateTime;
            this.itsCharge = charge;
            
        }

        public override void Execute()
        {
            Employee e = database.GetUnionMember(itsMemberId);

            if (e != null)
            {
                UnionAffiliation ua = null;
                if (e.Affiliation is UnionAffiliation)
                    ua = e.Affiliation as UnionAffiliation;

                if (ua != null)
                    ua.AddServiceCharge(
                        new ServiceCharge(itsDate, itsCharge));
                else
                    throw new ApplicationException(
                        "Tries to add service charge to union"
                        + "member without a union affiliation");
            }
            else
                throw new ApplicationException(
                    "No such union member.");
        }
    }
}