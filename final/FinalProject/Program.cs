using System;
using System.Collections.Generic;

public class Expense
{
    // Properties
    public decimal Amount { get; set; }
    public string Category { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }

    // Constructor
    public Expense(decimal amount, string category, DateTime date, string description)
    {
        Amount = amount;
        Category = category;
        Date = date;
        Description = description;
    }
}

public class ExpenseTracker
{
    // List to store expenses
    private List<Expense> expenses;

    // Constructor
    public ExpenseTracker()
    {
        expenses = new List<Expense>();
    }

    // Method to add an expense
    public void AddExpense(Expense expense)
    {
        expenses.Add(expense);
    }

    // Method to remove an expense
    public void RemoveExpense(Expense expense)
    {
        expenses.Remove(expense);
    }

    // Method to list expenses
    public void ListExpenses()
    {
        foreach (Expense expense in expenses)
        {
            Console.WriteLine($"Amount: {expense.Amount}, Category: {expense.Category}, Date: {expense.Date}, Description: {expense.Description}");
        }
    }

    // Method to calculate total expenses
    public decimal CalculateTotalExpenses()
    {
        decimal total = 0;
        foreach (Expense expense in expenses)
        {
            total += expense.Amount;
        }
        return total;
    }
}

class Program
{
    static void Main(string[] args)
    {
        ExpenseTracker tracker = new ExpenseTracker();

        // Allow the user to input expenses
        while (true)
        {
            Console.WriteLine("Enter expense details (amount category date(YYYY-MM-DD) description), or type 'done' to finish:");
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
                        Expense expense = new Expense(amount, parts[1], date, parts[3]);
                        tracker.AddExpense(expense);
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

        // Listing expenses
        Console.WriteLine("Expenses:");
        tracker.ListExpenses();

        // Calculating total expenses
        decimal totalExpenses = tracker.CalculateTotalExpenses();
        Console.WriteLine($"Total expenses: {totalExpenses}");
    }
}
