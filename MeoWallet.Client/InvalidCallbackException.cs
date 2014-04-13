namespace MeoWallet
{
    public class InvalidCallbackException : MeoWalletException
    {
        public InvalidCallbackException()
            : base("Invalid MEO Wallet callback. This may have been an attempt to forge a payment callback.")
        {
        }
    }
}