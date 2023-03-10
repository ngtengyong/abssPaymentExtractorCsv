using System;
using System.Text;

namespace AbssMaybankPayment
{
    class Program
    {
        private static string GetHiddenConsoleInput()
        {
            StringBuilder input = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
                else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
            }
            return input.ToString();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("This program extracts payment data from ABSS and export it as Maybank online payment csv file");
            Console.WriteLine("---------------------------------------------------------------------------------------------");

            try
            {
                Console.WriteLine("Enter Abss Username");
                var username = Console.ReadLine();
                if (username.Trim().Length == 0)
                {
                    Console.WriteLine("Username cannot be blank");
                    return;
                }

                Console.WriteLine("Enter Abss Password");
                var password = GetHiddenConsoleInput();
                //if (password.Trim().Length == 0)
                //{
                //    Console.WriteLine("Password cannot be blank");
                //    return;
                //}

                Console.WriteLine("Enter Account Code of the Paying Bank (i.e. 1-2110)");
                var bankAc = Console.ReadLine();
                if (bankAc.Trim().Length ==0)
                {
                    Console.WriteLine("Account Code must not be blank");
                    return;
                }

                Console.WriteLine("Please enter Payment Year [yyy-MM-dd] example : 2018-09-20 >");
                var dateText = Console.ReadLine();
                
                var isDateOk = DateTime.TryParse(dateText, out DateTime paydate);
                if (isDateOk)
                {
                    Console.WriteLine($"Extract Payment detail for date {paydate.ToString("yyyy-MM-dd")}");
                    SupplierPaymentExtractor ext = new SupplierPaymentExtractor(username, password, bankAc);
                    var lineCount = ext.Run(paydate);
                    Console.WriteLine($"Successfully export {lineCount} lines");
                }
                else
                    Console.WriteLine("Invalid input date");

                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
