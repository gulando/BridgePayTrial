namespace Payment.Interfaces.Models.Request
{
    public class ConfirmPaymentRequest
    {
        public string TransactionId { get; set; }

        public decimal PaRes { get; set; }
    }
}
