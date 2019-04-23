﻿using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class HourlyClassification : PaymentClassification
    {
        private readonly double hourlyRate;
        private readonly Dictionary<DateTime, TimeCard> timeCards;

        public HourlyClassification(double rate)
        {
            this.hourlyRate = rate;
            timeCards = new Dictionary<DateTime, TimeCard>();
        }

        public double HourlyRate
        {
            get { return hourlyRate; }
        }

        public TimeCard GetTimeCard(DateTime date)
        {
            return timeCards[date] as TimeCard;
        }

        public void AddTimeCard(TimeCard card)
        {
            timeCards[card.Date] = card;
        }

        public override double CalculatePay(Paycheck paycheck)
        {
            double totalPay = 0.0;
            foreach (TimeCard timeCard in timeCards.Values)
            {
                if (DateUtil.IsInPayPeriod(timeCard.Date,
                    paycheck.PayPeriodStartDate,
                    paycheck.PayPeriodEndDate))
                    totalPay += CalculatePayForTimeCard(timeCard);
            }
            return totalPay;
        }

        private double CalculatePayForTimeCard(TimeCard card)
        {
            double overtimeHours = Math.Max(0.0, card.Hours - 8);
            double normalHours = card.Hours - overtimeHours;
            return hourlyRate * normalHours +
                hourlyRate * 1.5 * overtimeHours;
        }

        public override string ToString()
        {
            return String.Format("${0}/hr", hourlyRate);
        }
    }
}