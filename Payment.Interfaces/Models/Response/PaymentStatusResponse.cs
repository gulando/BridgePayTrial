﻿using Payment.Interfaces.Enums;

namespace Payment.Interfaces.Models.Response
{
    public class PaymentStatusResponse
    {
        public string TransactionId { get; set; }

        public Status Status { get; set; }

        public double Amount { get; set; }

        public string Currency { get; set; }

        public string OrderId { get; set; }

        public string LastFourDigits { get; set; }
    }
}
