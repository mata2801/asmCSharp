using System;
using ConsoleApp3.entity;
using ConsoleApp3.model;

namespace ConsoleApp3
{
    public class GiaoDichSHB : GiaoDich
    {
        private static SHBAccountModel shbAccountModel;
        private GiaoDich _giaoDichImplementation;

        public GiaoDichSHB()
        {
            shbAccountModel = new SHBAccountModel();
        }

        public bool guiTien()
        {
            throw new NotImplementedException();
        }

        public bool rutTien()
        {
            throw new NotImplementedException();
        }

        public bool chuyenKhoan()
        {
            throw new NotImplementedException();
        }

        public void ChuyenKhoan()
        {
            _giaoDichImplementation.ChuyenKhoan();
        }

        public void Login()
        {
            Program.currentLoggedInAccount = null;
            Console.Clear();
            Console.WriteLine("Tien hanh dang nhap he thong SHB.");
            Console.WriteLine("Vui long nhap ten dang nhap: ");
            var username = Console.ReadLine();
            Console.WriteLine("Vui long nhap mat khau: ");
            var password = Console.ReadLine();
            SHBAccount shbAccount = shbAccountModel.FindByUsernameAndPassword(username, password);
            if (shbAccount == null)
            {
                Console.WriteLine("Sai thong tin tai khoan, vui long dang nhap lai.");
                Console.WriteLine("An phim bat ki de tiep tuc.");
                Console.Read();
                return;
            }

            Program.currentLoggedInAccount = shbAccount;
        }

        public void RutTien()
        {
            if (Program.currentLoggedInAccount != null)
            {
                Console.Clear();
                Console.WriteLine("Tien hanh rut tien tai he thong SHB.");
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
                    Message = "Tien hanh rut tien tai ATM voi so tien: " + amount,
                    Amount = amount,
                    CreatedAtMLS = DateTime.Now.Ticks,
                    UpdatedAtMLS = DateTime.Now.Ticks,
                    Status = 1
                };
                bool result = shbAccountModel.UpdateBalance(Program.currentLoggedInAccount, transaction);
            }
            else
            {
                Console.WriteLine("Vui long dang nhap de su dung chuc nang nay.");
            }
        }

        public void GuiTien()
        {

            if (Program.currentLoggedInAccount != null)
            {
                Console.Clear();
                Console.WriteLine("Tien hanh gui tien tai SHB.");
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
                    Message = "TIen hanh gui tien tai ATM voi so tien: " + amount,
                    Amount = amount,
                    CreatedAtMLS = DateTime.Now.Ticks,
                    UpdatedAtMLS = DateTime.Now.Ticks,
                    Status = 2
                };
                bool result = shbAccountModel.UpdateBalance(Program.currentLoggedInAccount, transaction);
            }
            else
            {
                Console.WriteLine("Vui long dang nhap de su dung chuc nang nay.");
            }

            void ChuyenKhoan()
            {

                if (Program.currentLoggedInAccount != null)
                {
                    Console.Clear();
                    Console.WriteLine("Tien hanh chuyen khoan tai he thong SHB.");
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
                        Message = "Tien hanh chuyen khoan tai ATM voi so tien: " + amount,
                        Amount = amount,
                        CreatedAtMLS = DateTime.Now.Ticks,
                        UpdatedAtMLS = DateTime.Now.Ticks,
                        Status = 3
                    };
                    bool result = shbAccountModel.UpdateBalance(Program.currentLoggedInAccount, transaction);
                }
                else
                {
                    Console.WriteLine("Vui long dang nhap de su dung chuc nang nay.");
                }
            }
        }
    }
}