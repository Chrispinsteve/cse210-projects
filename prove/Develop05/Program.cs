using System;
using System.Collections.Generic;
using System.IO;

public abstract class Goal
{
    protected string name;
    protected string description;
    protected int points;
    protected bool completed;

    public Goal(string name, string description, int points)
    {
        this.name = name;
        this.description = description;
        this.points = points;
        this.completed = false;
    }

    public virtual int RecordEvent()
    {
        if (!completed)
        {
            completed = true;
            return points;
        }
        return 0;
    }

    public bool IsCompleted()
    {
        return completed;
    }

    public override string ToString()
    {
        return $"{name} - {description} - {(completed ? "[X]" : "[ ]")}";
    }
}

public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
    }
}

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points)
    {
    }

    public override int RecordEvent()
    {
        completed = false; // Eternal goals are never completed
        return points;
    }
}

public class ChecklistGoal : Goal
{
    private int targetCount;
    private int bonus;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonus) : base(name, description, points)
    {
        this.targetCount = targetCount;
        this.bonus = bonus;
    }

    public override int RecordEvent()
    {
        if (!completed)
        {
            completed = true;
            return points + (completed ? bonus : 0);
        }
        return 0;
    }

    public override string ToString()
    {
        return $"{name} - {description} - {(completed ? "Completed" : "Incomplete")} - {targetCount} times for bonus - Bonus: {bonus}";
    }
}

public class GoalTracker
{
    private List<Goal> goals;
    private int score;

    public GoalTracker()
    {
        goals = new List<Goal>();
        score = 0;
    }

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void ListGoals()
    {
        foreach (Goal goal in goals)
        {
            Console.WriteLine(goal);
        }
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Goal goal in goals)
            {
                writer.WriteLine($"{goal.GetType().Name},{goal.name},{goal.description},{goal.points},{goal.IsCompleted()}");
            }
        }
    }

    public void LoadGoals(string filename)
    {
        goals.Clear();
        using (StreamReader reader = new StreamReader(filename))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                string type = parts[0];
                string name = parts[1];
                string description = parts[2];
                int points = int.Parse(parts[3]);
                bool completed = bool.Parse(parts[4]);
                Goal goal;
                if (type == nameof(SimpleGoal))
                {
                    goal = new SimpleGoal(name, description, points);
                }
                else if (type == nameof(EternalGoal))
                {
                    goal = new EternalGoal(name, description, points);
                }
                else if (type == nameof(ChecklistGoal))
                {
                    int targetCount = int.Parse(description);
                    int bonus = points;
                    goal = new ChecklistGoal(name, "Checklist Goal", points, targetCount, bonus);
                }
                else
                {
                    throw new ArgumentException($"Invalid goal type: {type}");
                }
                goal.IsCompleted();
                goals.Add(goal);
            }
        }
    }

    public void RecordEvent()
    {
        Console.WriteLine("Which Goal did you accomplish?");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].name}");
        }

        int choice;
        if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= goals.Count)
        {
            Goal goal = goals[choice - 1];
            int pointsEarned = goal.RecordEvent();
            score += pointsEarned;
            Console.WriteLine($"Congratulations!!! You have earned {pointsEarned} points!");
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }

    public void DisplayScore()
    {
        Console.WriteLine($"Score: {score}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        GoalTracker tracker = new GoalTracker();

        while (true)
        {
            Console.WriteLine("Menu options:");
            Console.WriteLine("1. Create new Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");

            Console.Write("Select a Choice from the menu: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Type of goals:");
                    Console.WriteLine("1. Simple Goal");
                    Console.WriteLine("2. Eternal Goal");
                    Console.WriteLine("3. Checklist Goal");
                    Console.Write("Which type of goal would you like to create? ");
                    string goalType = Console.ReadLine();
                    Console.Write("What is the name of your Goal? ");
                    string goalName = Console.ReadLine();
                    Console.Write("What is a short description? ");
                    string description = Console.ReadLine();
                    Console.Write("What is the amount of points associated with this goal? ");
                    int points = int.Parse(Console.ReadLine());
                    switch (goalType)
                    {
                        case "1":
                            tracker.AddGoal(new SimpleGoal(goalName, description, points));
                            break;
                        case "2":
                            tracker.AddGoal(new EternalGoal(goalName, description, points));
                            break;
                        case "3":
                            Console.Write("How many times does this Goal need to be accomplished for a bonus? ");
                            int targetCount = int.Parse(Console.ReadLine());
                            Console.Write("What is the bonus for accomplishing it that many times? ");
                            int bonus = int.Parse(Console.ReadLine());
                            tracker.AddGoal(new ChecklistGoal(goalName, description, points, targetCount, bonus));
                            break;
                        default:
                            Console.WriteLine("Invalid goal type.");
                            break;
                    }
                    break;
                case "2":
                    Console.WriteLine("List of Goals:");
                    tracker.ListGoals();
                    break;
                case "3":
                    Console.Write("What is the filename for the goal file? ");
                    string saveFilename = Console.ReadLine();
                    tracker.SaveGoals(saveFilename);
                    break;
                case "4":
                    Console.Write("What is the filename for the goal file? ");
                    string loadFilename = Console.ReadLine();
                    tracker.LoadGoals(loadFilename);
                    break;
                case "5":
                    tracker.RecordEvent();
                    break;
                case "6":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}
