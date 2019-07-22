namespace ConsoleApp3.entity
{
    public class BlockchainTransaction
    {
        public string TransactionID { get; set; }
        public string SenderAddress { get; set; }
        public static string ReceiverAddress { get; set; }
        public double Amount { get; set; }
        public long CreatedAtMLS { get; set; }
        public long UpdatedAtMLS { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        


        public BlockchainTransaction(string transactionId, string senderAddress, string receiverAddress, double amount)
        {
            TransactionID = transactionId;
            SenderAddress = senderAddress;
            ReceiverAddress = receiverAddress;
            Amount = amount;
        }
    }
}