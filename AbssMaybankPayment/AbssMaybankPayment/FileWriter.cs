using System;
using System.Collections.Generic;
using System.IO;

namespace AbssMaybankPayment
{
    public static class FileWriter
    {
        const string DELIMITER = ",";
        const string FILEEXT = "csv";

        public static int WritePaymentFile(List<Payment> paylist)
        {
            var lineCount = 0;
            var currentPath = $"{Directory.GetCurrentDirectory()}\\output";
            var filename = $"{DateTime.Now.ToString("yyyyMMdd-HHmmss")}.{FILEEXT}";
            Directory.CreateDirectory(currentPath);

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(currentPath, filename)))
            {
                var header = $"Date" +
                    $"{DELIMITER}Payment Voucher No" +
                    $"{DELIMITER}Company Number" +
                    $"{DELIMITER}Company Name" +
                    $"{DELIMITER}Amount" +
                    $"{DELIMITER}Swift Code" +
                    $"{DELIMITER}Bank Code" +
                    $"{DELIMITER}Bank Account No" +
                    $"{DELIMITER}Bank Description" +
                    "\r\n";

                outputFile.Write(header);

                foreach (var pay in paylist)
                {
                    var line = $"\"{pay.Date.ToString("yyyy-MM-dd")}\"" +
                        $"{DELIMITER}\"{pay.PayVoucher}\"" +
                        $"{DELIMITER}\"{pay.CompanyNo}\"" +
                        $"{DELIMITER}\"{pay.CompanyName}\"" +
                        $"{DELIMITER}\"{pay.Amount}\"" +
                        $"{DELIMITER}\"{pay.SwiftCode}\"" +
                        $"{DELIMITER}\"{pay.BankCode}\"" +
                        $"{DELIMITER}\"{pay.BankAccount}\"" +
                        $"{DELIMITER}";

                    if (string.IsNullOrEmpty(pay.BankCode) || string.IsNullOrEmpty(pay.BankAccount))
                        line += $"\"{pay.BankDescription}\"";
                    line += "\r\n";

                    outputFile.Write(line);
                }
                lineCount = paylist.Count;

            }

            return lineCount;
        }
    }
}
