﻿using CryptoPortfolio.Business.Contracts.CryptoBits;
using System;
using System.Collections.Generic;

namespace CryptoPortfolio.Business.Builder.Interfaces.CryptoPortfolio
{
    public interface ITransactionBuilder
    {
        IEnumerable<Transaction> GetTransactions();

        IEnumerable<Transaction> GetTransactionBySymbol(string symbol);

        IEnumerable<Transaction> GetTransactionByLocation(string location);

        IEnumerable<Transaction> GetTransactionsBySymbolAndLocation(string symbol, string location);

        IEnumerable<Transaction> GetTransactionsByDate(DateTime from);

        IEnumerable<Transaction> GetTransactionsByDate(DateTime from, DateTime to);

        bool AddNewTransaction(NewTransaction newTransaction);

        bool AddTransaction(Transaction newTransaction);

        bool AddTransactions(List<Transaction> transactions);
        
        bool UpdateTransaction(Transaction transaction);

        bool UpdateTransactions(List<Transaction> transactions);
    }
}
