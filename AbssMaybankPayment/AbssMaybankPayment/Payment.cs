using System;

namespace AbssMaybankPayment
{
    public class Payment
    {
        public string PayVoucher { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string CompanyNo { get; set; }
        public string CompanyName { get; set; }
        public string BankCode { get; set; }
        public string SwiftCode { get; set; }
        public string BankAccount { get; set; }
        public string BankDescription { get; set; }
    }
}

