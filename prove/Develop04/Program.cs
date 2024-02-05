using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to the Mindfulness App!");
        Console.WriteLine("Choose an activity:");
        Console.WriteLine("1. Breathing\n2. Reflection\n3. Listing");

        int choice;
        if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 3)
        {
            Console.Write("Enter the duration in seconds: ");
            int duration;
            if (int.TryParse(Console.ReadLine(), out duration) && duration > 0)
            {
                Activity activity;
                switch (choice)
                {
                    case 1:
                        activity = new BreathingActivity(duration);
                        break;
                    case 2:
                        activity = new ReflectionActivity(duration);
                        break;
                    case 3:
                        activity = new ListingActivity(duration);
                        break;
                    default:
                        activity = null;
                        break;
                }

                if (activity != null)
                {
                    activity.Start();
                }
                else
                {
                    Console.WriteLine("Invalid choice. Exiting.");
                }
            }
            else
            {
                Console.WriteLine("Invalid duration. Exiting.");
            }
        }
        else
        {
            Console.WriteLine("Invalid choice. Exiting.");
        }
    }
}

abstract class Activity
{
    protected string _name;
    protected string _description;
    protected int Duration;

    public Activity(string name, string description, int duration)
    {
        _name = name;
        _description = description;
        Duration = duration;
    }

    public void Start()
    {
        Console.WriteLine($"Starting {_name} - {_description}");
        PrepareToBegin();
        PerformActivity();
        Finish();
    }

    protected virtual void PrepareToBegin()
    {
        ShowAnimation("Get ready to begin...");
        Thread.Sleep(3000);
    }

    protected virtual void Finish()
    {
        Console.WriteLine("Good job! You have completed the activity.");
        ShowAnimation($"Activity completed: {_name} - Duration: {Duration} seconds");
        Thread.Sleep(3000);
    }

    protected abstract void PerformActivity();

    protected void ShowAnimation(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
        Thread.Sleep(1000);
    }
}

class BreathingActivity : Activity
{
    public BreathingActivity(int duration) : base("Breathing", "This activity will help you relax by walking you through breathing.", duration) { }

    protected override void PerformActivity()
    {
        Console.WriteLine("Clear your mind and focus on your breathing.");
        for (int i = 0; i < Duration; i++)
        {
            ShowAnimation("Breathe in...");
            Thread.Sleep(2000);
            ShowAnimation("Breathe out...");
            Thread.Sleep(2000);
        }
    }
}
class ReflectionActivity : Activity
{
    private readonly string[] _prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private readonly string[] _questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity(int duration) : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience.", duration) { }

    protected override void PerformActivity()
    {
        Console.WriteLine("Reflect on times in your life when you have shown strength and resilience.");
        for (int i = 0; i < Duration; i++)
        {
            string prompt = _prompts[i % _prompts.Length];
            Console.WriteLine(prompt);
            ShowAnimation("Reflecting...");
            Thread.Sleep(2000);

            foreach (string question in _questions)
            {
                Console.WriteLine(question);
                ShowAnimation("Pausing...");
                Thread.Sleep(2000);
            }
        }
    }
}

class ListingActivity : Activity
{
    private readonly string[] _prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity(int duration) : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.", duration) { }

    protected override void PerformActivity()
    {
        string prompt = _prompts[0]; // Choose the first prompt for simplicity
        Console.WriteLine($"{_description} - {prompt}");
        ShowAnimation("Get ready to list...");
        Thread.Sleep(3000);

        Console.WriteLine("Start listing items.");
        int itemsCount = 0;
        DateTime startTime = DateTime.Now;

        while ((DateTime.Now - startTime).TotalSeconds < Duration)
        {
            string item = Console.ReadLine();
            if (item.ToLower() == "done")
            {
                break;
            }
            itemsCount++;
        }

        Console.WriteLine($"You listed {itemsCount} items.");
        ShowAnimation($"Listing completed: {_name} - Duration: {Duration} seconds");
        Thread.Sleep(3000);
    }
}
