using System;
using System.Net;

namespace Payment.Api.Models
{
    public class PaymentException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public override string Message { get; }

        public PaymentException(HttpStatusCode status, string message = default)
        {
            StatusCode = status;
            Message = message;
        }
    }
}
