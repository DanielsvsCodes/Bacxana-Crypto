using System;
using Bacxana.Blocksystem;
using Bacxana.Transactions;
using Bacxana.Cryptography;
using Bacxana.Networking;

namespace Bacxana
{
    class Program
    {
        static void Main(string[] args)
        {
            Ledger blockchain = new Ledger();

            Wallet walletA = new Wallet();
            Wallet walletB = new Wallet();

            Console.WriteLine($"Wallet A Public Key: {walletA.PublicKey}");
            Console.WriteLine($"Wallet B Public Key: {walletB.PublicKey}");

            Transaction transaction1 = new Transaction(walletA.PublicKey, walletB.PublicKey, 10);
            transaction1.SignTransaction(walletA);

            if (transaction1.Signature != null)
            {
                bool isSignatureValid = Wallet.VerifySignature(walletA.PublicKey, transaction1.CalculateTransactionHash(), transaction1.Signature);
                Console.WriteLine($"Is transaction signature valid? {isSignatureValid}");
            }
            else
            {
                Console.WriteLine("Transaction signature is null, cannot verify.");
            }

            blockchain.CreateTransaction(transaction1);

            blockchain.MinePendingTransactions(walletA.PublicKey);

            Console.WriteLine($"Balance of Wallet A: {blockchain.GetBalance(walletA.PublicKey)}");
            Console.WriteLine($"Balance of Wallet B: {blockchain.GetBalance(walletB.PublicKey)}");

            Console.WriteLine("Blockchain is valid: " + blockchain.IsValid());

            Node node1 = new Node("127.0.0.1", blockchain);
            Node node2 = new Node("127.0.0.1", blockchain);
            Node node3 = new Node("127.0.0.1", blockchain);

            node1.ConnectToPeer(node2);
            node2.ConnectToPeer(node3);
            node3.ConnectToPeer(node1);

            node1.Start(9000);
            node2.Start(9001);
            node3.Start(9002);

            node1.Broadcast("New transaction added to the blockchain");

            Console.WriteLine("Network initialized and nodes started. Press Enter to exit...");
            Console.ReadLine();
        }
    }
}
