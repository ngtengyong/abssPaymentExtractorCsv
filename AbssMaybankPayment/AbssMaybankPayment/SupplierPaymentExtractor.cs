using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Odbc;
using System.Text.RegularExpressions;

namespace AbssMaybankPayment
{
    public class SupplierPaymentExtractor
    {
        const string CONFIG_DSN = "AbssDSN"; //The key in App.config that stores Abss DSN name

        private readonly string _dsnName;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _bankCOACode;

        public SupplierPaymentExtractor(string username, string password, string bankCoa)
        {
            _dsnName = ConfigurationManager.AppSettings.Get(CONFIG_DSN);
            _userName = username.Trim();
            _password = password.Trim();
            _bankCOACode = bankCoa.Trim();
        }

        private string ExtractBankInfo(string bankDesc, string regex)
        {
            var bankInfo = "";
            var rxBankCode = new Regex(regex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var match = rxBankCode.Match(bankDesc);
            var output = match.Groups[0].ToString();
            if (output.Length > 0)
                bankInfo = output.Trim();

            return bankInfo;
        }

        private string GetBankCode(string bankDesc)
        {
            return ExtractBankInfo(bankDesc, "[a-zA-Z]+");
        }

        private string GetBankAccount(string bankDesc)
        {
            return ExtractBankInfo(bankDesc, "[0-9]+");
        }

        public int Run(DateTime date)
        {
            var lineCount = 0;
            var dateText = date.ToString("yyyy-MM-dd");

            OdbcConnection cn;
            OdbcCommand cmd;

            var sql = "select " +
                "sup.SupplierPaymentNumber" +
                ",sup.Date" +
                ",sup.TotalSupplierPayment" +
                ",cd.CardIdentification" +
                ",cd.Name" +
                ",ad.Phone3" +
                ",ad.Salutation " +
                "from " +
                "SupplierPayments sup " +
                "inner join Cards cd on sup.CardRecordID = cd.CardRecordID " +
                "inner join Accounts ac on sup.IssuingAccountID = ac.AccountID " +
                "left outer join Address ad on cd.CardRecordID = ad.CardRecordID " +
                $"where sup.Date = '{dateText}' and ad.Location = 1 and ac.AccountNumber = '{_bankCOACode}'";
             /*
              1) "cd.IdentifierID like '%P%'". if User check the checkbox of Asia Paylink in Identifier field in Abss Card, there will be a "P" char in Identifier ID field 
              2) "ad.Location = 1". Abss allows user to maintain up to 5 addresses for each card. We will only use Address 1
             */


            cn = new OdbcConnection($"dsn={_dsnName};UID={_userName};PWD={_password};");
            cmd = new OdbcCommand(sql, cn);

            try
            {
                List<Payment> payList = new List<Payment>();
                cn.Open();
                OdbcDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var bankDesc = Convert.ToString(reader[5]).Trim();
                    var payment = new Payment
                    {
                        Date = Convert.ToDateTime(reader[1]),
                        PayVoucher = Convert.ToString(reader[0]),
                        Amount = Convert.ToDecimal(reader[2]),
                        CompanyNo = Convert.ToString(reader[3]),
                        CompanyName = Convert.ToString(reader[4]),
                        SwiftCode = Convert.ToString(reader[6]),
                        BankCode = GetBankCode(bankDesc),
                        BankAccount = GetBankAccount(bankDesc),
                        BankDescription = bankDesc
                    };
                    payList.Add(payment);
                }
                reader.Close();
                lineCount = FileWriter.WritePaymentFile(payList);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lineCount;
        }
    }
}
