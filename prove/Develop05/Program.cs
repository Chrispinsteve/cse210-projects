using System;
using System.Collections.Generic;

public class Transaction
{
    public decimal Amount { get; set; }
    public string Category { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }

    public Transaction(decimal amount, string category, DateTime date, string description)
    {
        Amount = amount;
        Category = category;
        Date = date;
        Description = description;
    }

    public virtual decimal GetAmount()
    {
        return Amount;
    }
}

public class Expense : Transaction
{
    public Expense(decimal amount, string category, DateTime date, string description) : base(amount, category, date, description) { }

    // Override GetAmount method to return negative amount for expenses
    public override decimal GetAmount()
    {
        return -Amount;
    }
}

public class Income : Transaction
{
    public Income(decimal amount, string category, DateTime date, string description) : base(amount, category, date, description) { }
}

public class TransactionTracker
{
    private List<Transaction> transactions;

    public TransactionTracker()
    {
        transactions = new List<Transaction>();
    }

    public void AddTransaction(Transaction transaction)
    {
        transactions.Add(transaction);
    }

    public void RemoveTransaction(Transaction transaction)
    {
        transactions.Remove(transaction);
    }

    public void ListTransactions()
    {
        foreach (Transaction transaction in transactions)
        {
            Console.WriteLine($"Amount: {transaction.Amount}, Category: {transaction.Category}, Date: {transaction.Date}, Description: {transaction.Description}");
        }
    }

    public decimal CalculateTotalAmount()
    {
        decimal total = 0;
        foreach (Transaction transaction in transactions)
        {
            total += transaction.GetAmount(); // Polymorphism in action
        }
        return total;
    }
}

class Program
{
    static void Main(string[] args)
    {
        TransactionTracker tracker = new TransactionTracker();

        // Allow the user to input transactions
        while (true)
        {
            Console.WriteLine("Enter transaction details (amount category date(YYYY-MM-DD) description), or type 'done' to finish:");
            string input = Console.ReadLine();

            if (input.ToLower() == "done")
                break;

            string[] parts = input.Split(' ');
            if (parts.Length == 4)
            {
                decimal amount;
                if (decimal.TryParse(parts[0], out amount))
                {
                    DateTime date;
                    if (DateTime.TryParse(parts[2], out date))
                    {
                        Transaction transaction;
                        if (amount < 0)
                        {
                            transaction = new Expense(amount, parts[1], date, parts[3]);
                        }
                        else
                        {
                            transaction = new Income(amount, parts[1], date, parts[3]);
                        }
                        tracker.AddTransaction(transaction);
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid amount format. Please enter a number.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input format. Please enter amount, category, date (YYYY-MM-DD), and description separated by spaces.");
            }
        }

        // Listing transactions
        Console.WriteLine("Transactions:");
        tracker.ListTransactions();

        // Calculating total amount
        decimal totalAmount = tracker.CalculateTotalAmount();
        Console.WriteLine($"Total amount: {totalAmount}");
    }
}
