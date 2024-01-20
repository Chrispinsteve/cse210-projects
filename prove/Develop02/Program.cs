using System;
using System.Collections.Generic;
using System.IO;

class Entry
{
    public string Prompt {get; set;}
    public string Response {get; set;}
    public string Name{get; set;}  // EXCEED REQUIREMENT
    public DateTime Date {get; set;}

    public Entry(string prompt, string response, string name, DateTime date)
    {
        Prompt = prompt;
        Response = response;
        Name = name;
        Date = date;
    }

    public override string ToString()
    {
        return $"{Name} Prompt: {Prompt}\nResponse: {Response}\nDate: {Date}\n";
    }

    public string ToFileString()
    {
        return $"{Name}: {Prompt},{Response},{Date}\n";
    }

    public static Entry FromFileString(string fileString)
    {
        string[] parts = fileString.Split(',');
        string prompt = parts[0];
        string response = parts[1];
        string name = parts [2];
        DateTime date = DateTime.Parse(parts[3]);
        return new Entry(prompt, response, name, date);
    }
}

class Journal
{
    private List<Entry> entries;

    public Journal()
    {
        entries = new List<Entry>();
    }

    public void AddEntry(Entry entry)
    {
        entries.Add(entry);
    }

    public void DisplayEntries()
    {
        foreach (Entry entry in entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry entry in entries)
            {
                writer.Write(entry.ToFileString());
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        entries.Clear();
        using (StreamReader reader = new StreamReader(filename))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Entry entry = Entry.FromFileString(line);
                entries.Add(entry);
            }
        }
    }
}

class Program
{
    static Journal journal;

    static void WriteEntry()
    {
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine("Enter your name: ");
        string name = Console.ReadLine();
        Console.WriteLine(prompt);
        string response = Console.ReadLine();
        Entry entry = new Entry(prompt, response, name, DateTime.Now);
        journal.AddEntry(entry);
    }

    static void DisplayJournal()
    {
        journal.DisplayEntries();
    }

    static void SaveJournal()
    {
        Console.WriteLine("Enter a filename: ");
        string filename = Console.ReadLine();
        journal.SaveToFile(filename);
    }

    static void LoadJournal()
    {
        Console.WriteLine("Enter a filename: ");
        string filename = Console.ReadLine();
        journal.LoadFromFile(filename);
    }

    static void Main(string[] args)
    {
        journal = new Journal();

        while (true)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Write an entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal");
            Console.WriteLine("4. Load journal");
            Console.WriteLine("5. Quit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    WriteEntry();
                    break;
                case "2":
                    DisplayJournal();
                    break;
                case "3":
                    SaveJournal();
                    break;
                case "4":
                    LoadJournal();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static string[] prompts = new string[]
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };
}
