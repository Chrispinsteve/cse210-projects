using System;
using System.Collections.Generic;
using System.IO;

public abstract class Goal
{
    protected string name;
    protected int points;
    protected bool completed;

    public Goal(string name, int points)
    {
        this.name = name;
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
        return $"{name} - {(completed ? "[X]" : "[ ]")}";
    }
}

public class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points)
    {
    }
}

public class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points)
    {
    }

    public override int RecordEvent()
    {
        completed = false; 
        return points;
    }
}

public class ChecklistGoal : Goal
{
    private int targetCount;
    private int currentCount;

    public ChecklistGoal(string name, int points, int targetCount) : base(name, points)
    {
        this.targetCount = targetCount;
        this.currentCount = 0;
    }

    public override int RecordEvent()
    {
        if (!completed)
        {
            currentCount++;
            if (currentCount == targetCount)
            {
                completed = true;
                return points + 55;
            }
            return points;
        }
        return 0;
    }

    public override string ToString()
    {
        return $"{name} - {(completed ? "Completed" : "Incomplete")} {currentCount}/{targetCount}";
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

    public string RecordEvent(string goalName)
    {
        foreach (Goal goal in goals)
        {
            if (goal.ToString().StartsWith(goalName))
            {
                int points = goal.RecordEvent();
                score += points;
                return $"You earned {points} points for completing {goalName}.";
            }
        }
        return "Goal not found.";
    }

    public void DisplayGoals()
    {
        foreach (Goal goal in goals)
        {
            Console.WriteLine(goal);
        }
    }

    public void DisplayScore()
    {
        Console.WriteLine($"Your current score is: {score}");
    }

    public void SaveProgress(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Goal goal in goals)
            {
                writer.WriteLine($"{goal.ToString()},{goal.IsCompleted()}");
            }
        }
    }

    public void LoadProgress(string filename)
    {
        goals.Clear();
        using (StreamReader reader = new StreamReader(filename))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                string goalName = parts[0];
                bool completed = bool.Parse(parts[1]);
                Goal goal;
                if (goalName.EndsWith("(Checklist)"))
                {
                    string[] nameParts = goalName.Split('(');
                    string name = nameParts[0].Trim();
                    int targetCount = int.Parse(nameParts[1].Substring(0, nameParts[1].IndexOf(')')));
                    goal = new ChecklistGoal(name, 0, targetCount);
                }
                else if (completed)
                {
                    goal = new EternalGoal(goalName, 0);
                }
                else
                {
                    goal = new SimpleGoal(goalName, 0);
                }
                goal.IsCompleted();
                goals.Add(goal);
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        GoalTracker tracker = new GoalTracker();

        // Load previous progress if available
        tracker.LoadProgress("progress.txt");

        // Add goals
        tracker.AddGoal(new SimpleGoal("Run a marathon", 89));
        tracker.AddGoal(new EternalGoal("Read scriptures", 144));
        tracker.AddGoal(new ChecklistGoal("Attend the temple (Checklist)", 50, 10));

        // Record events
        Console.WriteLine(tracker.RecordEvent("Run a marathon"));
        Console.WriteLine(tracker.RecordEvent("Read scriptures"));
        Console.WriteLine(tracker.RecordEvent("Attend the temple (Checklist)"));
        Console.WriteLine(tracker.RecordEvent("Attend the temple (Checklist)"));
        Console.WriteLine(tracker.RecordEvent("Attend the temple (Checklist)"));

        // Display goals and score
        tracker.DisplayGoals();
        tracker.DisplayScore();

        // Save progress
        tracker.SaveProgress("progress.txt");
    }
}
