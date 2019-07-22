using System;
using ConsoleApp3.entity;
using MySql.Data.MySqlClient;

namespace ConsoleApp3.model
{
    public class SHBAccountModel
    {
       
        public SHBAccount FindByUsernameAndPassword(string username, string password)
        {
                       
            var cmd = new MySqlCommand("select * from SHBAccount where Username = @Username And Password = @Password",
                ConnectionHelper.GetConnection());
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);
            
            SHBAccount shbAccount = null;
                      
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                shbAccount = new SHBAccount
                {
                    Username = reader.GetString("username"),
                    Password = reader.GetString("password"),
                    Balance = reader.GetDouble("balance")
                };
            }

            ConnectionHelper.CloseConnection();
        
            return shbAccount;
        }

        public bool UpdateBalance(SHBAccount currentLoggedInAccount, SHBTransaction transaction)
        {
            ConnectionHelper.GetConnection();
            MySqlConnection mySqlConnection;
            var tran = ConnectionHelper.GetConnection().BeginTransaction();
            try
            {
                var cmd = new MySqlCommand("select * from SHBAccount where Username = @Username",
                    ConnectionHelper.GetConnection());
                cmd.Parameters.AddWithValue("@Username", currentLoggedInAccount.AccountNumber);
                SHBAccount shbAccount = null;
                var reader = cmd.ExecuteReader();
                double currentAccountBalance = 0;

                if (reader.Read())
                {
                    currentAccountBalance = reader.GetDouble("balance");
                }

                reader.Close();
                if (currentAccountBalance < 0)
                {
                    Console.WriteLine("Khong du tien trong tai khoan.");
                    return false;
                }

                if (transaction.Type == 1)
                {
                    if (currentAccountBalance < transaction.Amount)
                    {
                        Console.WriteLine("Khong du tien thuc hien giao dich");
                        return false;
                    }
                    currentAccountBalance -= transaction.Amount;
                }
                else if (transaction.Type == 2)
                {
                    currentAccountBalance += transaction.Amount;
                }

                var updateQuery =
                    "update `SHBAccount` set `balance` = @balance where accountNumber = @accountNumber";
                var sqlCmd = new MySqlCommand(updateQuery, ConnectionHelper.GetConnection());
                sqlCmd.Parameters.AddWithValue("@balance", currentAccountBalance);
                sqlCmd.Parameters.AddWithValue("@accountNumber", currentLoggedInAccount.AccountNumber);
                var updateResult = sqlCmd.ExecuteNonQuery();
                var historyTransactionQuery =
                    "insert into `SHBTransaction` (transactionId, type, senderAccountNumber, receiverAccountNumber, amount, message) " +
                    "values (@id, @type, @senderAccountNumber, @receiverAccountNumber, @amount, @message)";
                var historyTransactionCmd =
                    new MySqlCommand(historyTransactionQuery, ConnectionHelper.GetConnection());
                historyTransactionCmd.Parameters.AddWithValue("@id", transaction.TransactionId);
                historyTransactionCmd.Parameters.AddWithValue("@amount", transaction.Amount);
                historyTransactionCmd.Parameters.AddWithValue("@type", transaction.Type);
                historyTransactionCmd.Parameters.AddWithValue("@message", transaction.Message);
                historyTransactionCmd.Parameters.AddWithValue("@senderAccountNumber",
                    transaction.SenderAccountNumber);
                historyTransactionCmd.Parameters.AddWithValue("@receiverAccountNumber",
                    transaction.ReceiverAccountNumber);
                var historyResult = historyTransactionCmd.ExecuteNonQuery();

                if (updateResult != 1 || historyResult != 1)
                {
                    throw new Exception("Khong the giao dich hoac update tai khoan.");
                }

                tran.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                tran.Rollback();                
                return false;
            }

            ConnectionHelper.CloseConnection();
            return true;
        }
    }
}