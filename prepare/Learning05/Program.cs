using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Test Square class
        Square square = new Square("Red", 5);
        Console.WriteLine($"Square Color: {square.Color}, Area: {square.GetArea()}");

        // Test Rectangle class
        Rectangle rectangle = new Rectangle("Blue", 4, 6);
        Console.WriteLine($"Rectangle Color: {rectangle.Color}, Area: {rectangle.GetArea()}");

        // Test Circle class
        Circle circle = new Circle("Green", 3);
        Console.WriteLine($"Circle Color: {circle.Color}, Area: {circle.GetArea()}");

        // Build a List
        List<Shape> shapes = new List<Shape>();
        shapes.Add(square);
        shapes.Add(rectangle);
        shapes.Add(circle);

        // Iterate through the list and display information for each shape
        foreach (var shape in shapes)
        {
            Console.WriteLine($"Color: {shape.Color}, Area: {shape.GetArea()}");
        }
    }
}
