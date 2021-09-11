namespace Payment.Interfaces.Enums
{
    public enum Status
    {
        Init,
        Pending,
        Approved,
        Declined,
        DeclinedDueToInvalidCreditCard,
    }
}
