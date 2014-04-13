namespace MeoWallet
{
    public class MeoWalletApiException : MeoWalletException
    {
        public int Code { get; private set; }
        public string Reason { get; private set; }
        public string Link { get; private set; }
        public string Id { get; private set; }

        public MeoWalletApiException(Error error)
            : base(error.Message)
        {
            Code = error.Code;
            Reason = error.Reason;
            Link = error.Link;
            Id = error.Tid;
        }
    }
}