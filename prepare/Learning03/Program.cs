// Program.cs
using System;

class Program
{
    static void Main()
    {
        // Example usage

        // Using the first constructor (1/1)
        Fraction fraction1 = new Fraction();
        Console.WriteLine($"Fraction 1: {fraction1}");

        // Using the second constructor (6/1)
        Fraction fraction2 = new Fraction(6);
        Console.WriteLine($"Fraction 2: {fraction2}");

        // Using the third constructor (6/7)
        Fraction fraction3 = new Fraction(6, 7);
        Console.WriteLine($"Fraction 3: {fraction3}");

        // Testing getters and setters
        Console.WriteLine("\nTesting getters and setters:");

        // Get values using getters
        int topValue = fraction3.GetTop();
        int bottomValue = fraction3.GetBottom();
        Console.WriteLine($"Initial Values: Top = {topValue}, Bottom = {bottomValue}");

        // Set new values using setters
        fraction3.SetTop(8);
        fraction3.SetBottom(14);

        // Get values again using getters after setting new values
        topValue = fraction3.GetTop();
        bottomValue = fraction3.GetBottom();
        Console.WriteLine($"New Values: Top = {topValue}, Bottom = {bottomValue}");
    }
}
