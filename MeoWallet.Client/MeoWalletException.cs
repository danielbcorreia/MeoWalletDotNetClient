using System;

namespace MeoWallet
{
    public abstract class MeoWalletException : Exception
    {
        protected MeoWalletException()
        {
            
        }

        protected MeoWalletException(string message)
            : base(message)
        {
            
        }
    }
}