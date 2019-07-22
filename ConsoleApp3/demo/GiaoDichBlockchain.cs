using System;
using ConsoleApp3.entity;
using ConsoleApp3.model;

namespace ConsoleApp3
{
    public class GiaoDichBlockchain : GiaoDich
    {
        public bool guiTien()
        {
            throw new System.NotImplementedException();
        }

        public bool rutTien()
        {
            throw new System.NotImplementedException();
        }

        public bool chuyenKhoan()
        {
            throw new System.NotImplementedException();
        }


        public void ChuyenKhoan()
        {
            throw new NotImplementedException();
        }

        public void Login()
        {
            Program.currentLoggedInAccount = null;
            Console.Clear();
            Console.WriteLine("Tien hanh dang nhap he thong Blockchain.");
            Console.WriteLine("Vui lòng nhập address: ");
            var address = Console.ReadLine();
            Console.WriteLine("Vui long nhap private key: ");
            var privatekey = Console.ReadLine();
            var blockchainAccount = BlockchainAddressModel.FindByAddrssAndPrivateKey(address, privatekey);
            if (blockchainAccount == null)
            {
                Console.WriteLine("Sai dia chi, vui long nhap lai.");
                Console.WriteLine("An phim bat ki de tiep tuc.");
                Console.Read();
                return;
            }

            Program.currentLoggedInAccount = blockchainAccount as SHBAccount;

        }

        public void RutTien()
        {
            if (Program.currentLoggedInAccount != null)
            {
                Console.Clear();
                Console.WriteLine("Tien hanh rut tien tai he thong Blockchain.");
                Console.WriteLine("Vui long nhap so tien can rut.");
                var amount = double.Parse(Console.ReadLine());
                if (amount <= 0)
                {
                    Console.WriteLine("So luong khong hop le, vui long thu lai.");
                    return;
                }

                var transaction = new SHBTransaction
                {
                    TransactionId = Guid.NewGuid().ToString(),
                    SenderAccountNumber = Program.currentLoggedInAccount.AccountNumber,
                    ReceiverAccountNumber = Program.currentLoggedInAccount.AccountNumber,
                    Type = 1,
                    Message = "Tien hanh rut tien o ATM voi so tien: " + amount,
                    Amount = amount,
                    CreatedAtMLS = DateTime.Now.Ticks,
                    UpdatedAtMLS = DateTime.Now.Ticks,
                    Status = 1
                };
                BlockchainTransaction blockchainTransaction;
                bool result =
                    BlockchainAddressModel.UpdateBalance(Program.currentLoggedInAccount, typeof(BlockchainTransaction));
            }
            else
            {
                Console.WriteLine("Vui long dang nhap lai de su dung chuc nang nay.");
            }
        }

        public void GuiTien()
        {
            if (Program.currentLoggedInAccount != null)
            {
                Console.Clear();
                Console.WriteLine("Tien hanh gui tien tai he thong Blockchain.");
                Console.WriteLine("Vui long nhap so tien can gui.");
                var amount = double.Parse(Console.ReadLine());
                if (amount <= 0)
                {
                    Console.WriteLine("So luong khong hop le, vui long thu lai.");
                    return;
                }

                var transaction = new SHBTransaction
                {
                    TransactionId = Guid.NewGuid().ToString(),
                    SenderAccountNumber = Program.currentLoggedInAccount.AccountNumber,
                    ReceiverAccountNumber = Program.currentLoggedInAccount.AccountNumber,
                    Type = 2,
                    Message = "Tien hanh gui tien o ATM voi so tien: " + amount,
                    Amount = amount,
                    CreatedAtMLS = DateTime.Now.Ticks,
                    UpdatedAtMLS = DateTime.Now.Ticks,
                    Status = 2
                };
                BlockchainTransaction blockchainTransaction;
                bool result =
                    BlockchainAddressModel.UpdateBalance(Program.currentLoggedInAccount, typeof(BlockchainTransaction));
            }
            else
            {
                Console.WriteLine("Vui long dang nhap lai de su dung chuc nang nay.");
            }

            void ChuyenKhoan()
            {
                if (Program.currentLoggedInAccount != null)
                {
                    Console.Clear();
                    Console.WriteLine("Tien hanh chuyen khoan tai he thong BLockchain.");
                    Console.WriteLine("Vui long nhap so tien can chuyen khoan.");
                    var amount = double.Parse(Console.ReadLine());
                    if (amount <= 0)
                    {
                        Console.WriteLine("So luong khong hop le, vui long thu lai.");
                        return;
                    }

                    var transaction = new SHBTransaction
                    {
                        TransactionId = Guid.NewGuid().ToString(),
                        SenderAccountNumber = Program.currentLoggedInAccount.AccountNumber,
                        ReceiverAccountNumber = Program.currentLoggedInAccount.AccountNumber,
                        Type = 3,
                        Message = "Tien hanh chuyen khoan o ATM voi so tien: " + amount,
                        Amount = amount,
                        CreatedAtMLS = DateTime.Now.Ticks,
                        UpdatedAtMLS = DateTime.Now.Ticks,
                        Status = 3
                    };
                    BlockchainTransaction blockchainTransaction;
                    bool result = BlockchainAddressModel.UpdateBalance(Program.currentLoggedInAccount,
                        typeof(BlockchainTransaction));
                }
                else
                {
                    Console.WriteLine("Vui long dang nhap lai de su dung chuc nang nay.");
                }
            }







        }
    }
}
