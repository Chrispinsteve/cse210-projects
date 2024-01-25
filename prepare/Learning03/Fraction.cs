// Fraction.cs

using System;

public class Fraction
{
    private int top;
    private int bottom;

    // Constructor that initializes the fraction to 1/1
    public Fraction()
    {
        top = 1;
        bottom = 1;
    }

    //-----Constructor that has one parameter for the top and initializes the denominator to 1
    public Fraction(int top)
    {
        this.top = top;
        bottom = 1;
    }

    //-----Constructor that has two parameters, one for the top and one for the bottom
    public Fraction(int top, int bottom)
    {
        if (bottom == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.");
        }

        this.top = top;
        this.bottom = bottom;

        Simplify(); //-----Simplify the fraction after initialization
    }

    //------Method to simplify the fraction
    private void Simplify()
    {
        int gcd = GCD(Math.Abs(top), Math.Abs(bottom));

        top /= gcd;
        bottom /= gcd;

        //------Make sure the sign is on the top
        if (bottom < 0)
        {
            top = -top;
            bottom = -bottom;
        }
    }

    //------Method to calculate the greatest common divisor (GCD) using Euclidean algorithm
    private int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    //-------Getters and setters for the top and bottom values
    public int GetTop()
    {
        return top;
    }

    public void SetTop(int newTop)
    {
        top = newTop;
        Simplify();
    }

    public int GetBottom()
    {
        return bottom;
    }

    public void SetBottom(int newBottom)
    {
        if (newBottom == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.");
        }

        bottom = newBottom;
        Simplify();
    }

    //-------Method to convert the fraction to a string representation
    public override string ToString()
    {
        return $"{top}/{bottom}";
    }
}

