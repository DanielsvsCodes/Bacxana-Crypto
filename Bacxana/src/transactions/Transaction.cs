using System;
using System.Security.Cryptography;
using System.Text;
using Bacxana.Cryptography;

namespace Bacxana.Transactions
{
    public class Transaction
    {
        public string? Sender { get; set; }
        public string Recipient { get; set; }
        public decimal Amount { get; set; }
        public string? Signature { get; set; }

        public Transaction(string sender, string recipient, decimal amount)
        {
            Sender = sender;
            Recipient = recipient;
            Amount = amount;
            Signature = null;
        }

        public string CalculateTransactionHash()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var rawData = $"{Sender}{Recipient}{Amount}";
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return Convert.ToBase64String(bytes);
            }
        }

        public void SignTransaction(Wallet senderWallet)
        {
            if (senderWallet.PublicKey != Sender)
            {
                throw new Exception("You cannot sign transactions for other wallets!");
            }

            Signature = senderWallet.SignData(CalculateTransactionHash());
        }

        public bool IsValid()
        {
            if (Sender == null) return true;

            if (string.IsNullOrEmpty(Signature))
            {
                throw new Exception("No signature in this transaction.");
            }

            return Wallet.VerifySignature(Sender, CalculateTransactionHash(), Signature);
        }
    }
}
