namespace ConsoleApp3.entity
{
    public class SHBTransaction
    {
        public string TransactionId { get; set; }
        public int Type { get; set; } 
        public string SenderAccountNumber { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public double Amount { get; set; }
        public string Message { get; set; }
        public long CreatedAtMLS { get; set; }
        public long UpdatedAtMLS { get; set; }
        public int Status { get; set; }

        public SHBTransaction()
        {
        }

        public SHBTransaction(string senderAccountNumber, string receiverAccountNumber, double amount, string message)
        {
            SenderAccountNumber = senderAccountNumber;
            ReceiverAccountNumber = receiverAccountNumber;
            Amount = amount;
            Message = message;
        }
    }
}