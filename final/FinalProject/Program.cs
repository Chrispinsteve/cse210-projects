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
    public void RemoveExpense(int index)
    {
        if (index >= 0 && index < expenses.Count)
        {
            expenses.RemoveAt(index);
            Console.WriteLine("Expense removed successfully.");
        }
        else
        {
            Console.WriteLine("Invalid expense index.");
        }
    }

    // Method to list expenses
    public void ListExpenses()
    {
        if (expenses.Count == 0)
        {
            Console.WriteLine("No expenses recorded.");
        }
        else
        {
            Console.WriteLine("Expenses:");
            for (int i = 0; i < expenses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Amount: ${expenses[i].Amount}, Category: {expenses[i].Category}, Date: {expenses[i].Date.ToString("yyyy-MM-dd")}, Description: {expenses[i].Description}");
            }
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

        // Allow the user to input, list, remove expenses, or calculate total expenses
        while (true)
        {
            Console.WriteLine("Enter 'add' to input an expense, 'list' to list expenses, 'remove' to remove an expense, 'total' to calculate total expenses, or 'done' to finish:");
            string command = Console.ReadLine();

            if (command.ToLower() == "done")
                break;

            switch (command.ToLower())
            {
                case "add":
                    Console.WriteLine("Enter expense details (amount, category, date(YYYY-MM-DD), description):");
                    string input = Console.ReadLine();
                    string[] parts = input.Split(',');
                    if (parts.Length == 4)
                    {
                        decimal amount;
                        if (decimal.TryParse(parts[0], out amount))
                        {
                            DateTime date;
                            if (DateTime.TryParse(parts[2], out date))
                            {
                                Expense expense = new Expense(amount, parts[1].Trim(), date, parts[3].Trim());
                                tracker.AddExpense(expense);
                                Console.WriteLine("Expense added successfully.");
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
                        Console.WriteLine("Invalid input format. Please enter amount, category, date (YYYY-MM-DD), and description separated by commas.");
                    }
                    break;

                case "list":
                    tracker.ListExpenses();
                    break;

                case "remove":
                    Console.WriteLine("Enter the index of the expense to remove:");
                    if (int.TryParse(Console.ReadLine(), out int index))
                    {
                        tracker.RemoveExpense(index - 1);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid index.");
                    }
                    break;

                case "total":
                    decimal totalExpenses = tracker.CalculateTotalExpenses();
                    Console.WriteLine($"Total expenses: ${totalExpenses}");
                    break;

                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }
    }
}
