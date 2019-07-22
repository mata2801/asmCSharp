using System;
using System.Runtime.InteropServices;
using ConsoleApp3.entity;
using MySql.Data.MySqlClient;


namespace ConsoleApp3.model
{
    public class BlockchainAddressModel
    {
        public BlockchainAddress FindByAddressAndPrivateKey(string address, string privatekey)
        {
            
            var cmd = new MySqlCommand("select * from BlockchainAddress where Address = @Address And PrivateKey = @PrivateKey ",
                ConnectionHelper.GetConnection());
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@PrivateKey", privatekey);
            BlockchainAddress blockchainAddress = null;

            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                blockchainAddress = new BlockchainAddress();
                {
                    Address = reader.GetString("address");
                    PrivateKey = reader.GetString("privatekey");
                    Balance = reader.GetString("balance");

                };
                                                 

            }

            ConnectionHelper.GetConnection();

            
            return blockchainAddress;
        }

        private void GetConnection()
        {
            throw new NotImplementedException();
        }

        public string Balance { get; set; }

        public string PrivateKey { get; set; }

        public string Address { get; set; }

        public bool UpdateBalance(BlockchainAddress currentLoggedInAccount, BlockchainTransaction blockchainTransaction)
        {
            ConnectionHelper.GetConnection();
            MySqlConnection mySqlConnection;
            var tran = ConnectionHelper.GetConnection().BeginTransaction();
            try
            {
                var cmd = new MySqlCommand("select * from BlockchainAddress where Address = @Address",
                    ConnectionHelper.GetConnection());
                cmd.Parameters.AddWithValue("@Address", currentLoggedInAccount.Address);
                BlockchainAddress blockchainAddress = null;
                var reader = cmd.ExecuteReader();
                double currentAddressBalance = 0;

                if (reader.Read())
                {
                    currentAddressBalance = reader.GetDouble("balance");
                }
                reader.Close();
                if (currentAddressBalance < 0)
                {
                    Console.WriteLine("Khong du tien");
                    return false;
                }

                if ((blockchainTransaction.Type = 1) != 0)
                {
                    if (currentAddressBalance < blockchainTransaction.Amount)
                    {
                        Console.WriteLine("Khong du tien de thuc hien giao dich");
                        return false;
                    }

                    currentAddressBalance -= blockchainTransaction.Amount;
                }
                else if (blockchainTransaction.Type == 2)
                {
                    currentAddressBalance += blockchainTransaction.Amount;
                }

                var updateQuery =
                    "update `BlockchainAddress` set `balance` = @balance where Address = @Address";
                var sqlCmd = new MySqlCommand(updateQuery, ConnectionHelper.GetConnection());
                sqlCmd.Parameters.AddWithValue("@balance", currentAddressBalance);
                sqlCmd.Parameters.AddWithValue("@address", currentLoggedInAccount.Address);
                var updateResult = sqlCmd.ExecuteNonQuery();
                var historyTransactionQuery =
                    "insert into `BlockchainTransaction` (transactionId, type, senderAccountNumber, receiveAccountNumber, amount, status)" +
                    "values(@id, @type, @senderAccountNumber, @receiveAccountNumber, @amount, @status)";
                var historyTransactionCmd = new MySqlCommand(historyTransactionQuery, ConnectionHelper.GetConnection());
                historyTransactionCmd.Parameters.AddWithValue("@id", blockchainTransaction.TransactionID );
                historyTransactionCmd.Parameters.AddWithValue("@amount", blockchainTransaction.Amount);
                historyTransactionCmd.Parameters.AddWithValue("@type", blockchainTransaction.Type);
                historyTransactionCmd.Parameters.AddWithValue("@message", blockchainTransaction.Status);
                historyTransactionCmd.Parameters.AddWithValue("@senderAccountNumber",
                    blockchainTransaction.SenderAddress);
                historyTransactionCmd.Parameters.AddWithValue("@receiverAccountNumber",
                    BlockchainTransaction.ReceiverAddress);
                var historyResult = historyTransactionCmd.ExecuteNonQuery();
                if (updateResult != 1 || historyResult != 1)
                {
                    throw new Exception("Khong the giao dich hoac them tai khoan.");
                }
                tran.Commit();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                tran.Rollback();
                return false;
            }

            ConnectionHelper.GetConnection();
            return true;
        }

        public static object FindByAddrssAndPrivateKey(object address, object privatekey)
        {
            throw new NotImplementedException();
        }

        public static bool UpdateBalance(SHBAccount currentLoggedInAccount, object blockchainTransaction)
        {
            throw new NotImplementedException();
        }
    }
}