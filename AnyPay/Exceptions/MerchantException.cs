namespace AnyPay.Exceptions;

public class MerchantException : Exception
{
    public MerchantException(string message)
        : base(message) { }
}
