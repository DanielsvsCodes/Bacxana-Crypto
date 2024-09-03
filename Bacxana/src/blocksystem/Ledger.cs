using System;
using System.Collections.Generic;
using Bacxana.Transactions;

namespace Bacxana.Blocksystem
{
    public class Ledger
    {
        public IList<Block> Chain { get; private set; }
        public List<Transaction> PendingTransactions { get; private set; }
        public int Difficulty { get; set; } = 2;
        public decimal MiningReward { get; set; } = 1.0m;

        public Ledger()
        {
            Chain = new List<Block>();
            PendingTransactions = new List<Transaction>();
            AddGenesisBlock();
        }

        private void AddGenesisBlock()
        {
            var genesisBlock = new Block(0, new List<Transaction>(), "0");
            genesisBlock.MineBlock(Difficulty);
            Chain.Add(genesisBlock);
        }

        public Block GetLatestBlock()
        {
            return Chain[^1];
        }

        public void CreateTransaction(Transaction transaction)
        {
            PendingTransactions.Add(transaction);
        }

        public void MinePendingTransactions(string minerAddress)
        {
            var block = new Block(Chain.Count, new List<Transaction>(PendingTransactions), GetLatestBlock().Hash);
            block.MineBlock(Difficulty);

            Console.WriteLine("Block successfully mined!");
            Chain.Add(block);

            PendingTransactions = new List<Transaction>
            {
                new Transaction("SYSTEM", minerAddress, MiningReward)
            };
        }

        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                var currentBlock = Chain[i];
                var previousBlock = Chain[i - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }

                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }

            return true;
        }

        public decimal GetBalance(string address)
        {
            decimal balance = 0;

            foreach (var block in Chain)
            {
                foreach (var transaction in block.Transactions)
                {
                    if (transaction.Sender == address)
                    {
                        balance -= transaction.Amount;
                    }

                    if (transaction.Recipient == address)
                    {
                        balance += transaction.Amount;
                    }
                }
            }

            return balance;
        }
    }
}
