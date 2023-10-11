using System;

class Fraction
{
    private int whole;
    private int numerator;
    private uint denominator;

    public Fraction(int whole, int numerator, uint denominator)
    {
        this.whole = whole;
        this.numerator = numerator;
        this.denominator = denominator;
        Simplify();
    }

    public Fraction(int numerator, uint denominator)
    {
        this.whole = 0;
        this.numerator = numerator;
        this.denominator = denominator;
        Simplify();
    }

    private void Simplify()
    {
        int gcd = GCD(Math.Abs(numerator), (int)denominator);
        numerator /= gcd;
        denominator /= (uint)gcd;
        if (numerator >= denominator)
        {
            whole += numerator / (int)denominator;
            numerator %= (int)denominator;
        }
    }

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

    public int Whole
    {
        get { return whole; }
    }

    public int Numerator
    {
        get { return numerator; }
    }

    public uint Denominator
    {
        get { return denominator; }
    }

    public override string ToString()
    {
        if (whole != 0)
        {
            return $"{whole} {numerator}/{denominator}";
        }
        else if (numerator != 0)
        {
            return $"{numerator}/{denominator}";
        }
        else
        {
            return "0";
        }
    }

    public static Fraction operator +(Fraction a, Fraction b)
    {
        int newNumerator = a.whole * (int)a.denominator + a.numerator * (int)b.denominator;
        uint newDenominator = a.denominator * b.denominator;
        return new Fraction(newNumerator, newDenominator);
    }

    public static Fraction operator -(Fraction a, Fraction b)
    {
        int newNumerator = a.whole * (int)a.denominator - a.numerator * (int)b.denominator;
        uint newDenominator = a.denominator * b.denominator;
        return new Fraction(newNumerator, newDenominator);
    }

    public static Fraction operator *(Fraction a, Fraction b)
    {
        int newNumerator = (a.whole * (int)a.denominator + a.numerator) * (b.whole * (int)b.denominator + b.numerator);
        uint newDenominator = a.denominator * b.denominator;
        return new Fraction(newNumerator, newDenominator);
    }

    public static Fraction operator /(Fraction a, Fraction b)
    {
        int newNumerator = (a.whole * (int)a.denominator + a.numerator) * (int)b.denominator;
        uint newDenominator = a.denominator * b.denominator;
        return new Fraction(newNumerator, newDenominator);
    }

    public static Fraction operator ^(Fraction a, int power)
    {
        int newNumerator = (int)Math.Pow(a.whole * (int)a.denominator + a.numerator, power);
        uint newDenominator = (uint)Math.Pow((int)a.denominator, power);
        return new Fraction(newNumerator, newDenominator);
    }

    public static implicit operator double(Fraction fraction)
    {
        return (double)(fraction.whole * fraction.denominator + fraction.numerator) / fraction.denominator;
    }

    public static explicit operator Fraction(int value)
    {
        return new Fraction(value, 1);
    }

    public static explicit operator Fraction(double value)
    {
        int whole = (int)value;
        double fractionalPart = value - whole;
        const double epsilon = 1e-9;
        int numerator = 0;
        uint denominator = 1;
        double error = Math.Abs(fractionalPart);
        for (int i = 2; i <= 10000; i++)
        {
            double fraction = Math.Round(fractionalPart * i);
            double newError = Math.Abs(fraction / i - fractionalPart);
            if (newError < error && Math.Abs(fraction / i - fractionalPart) < epsilon)
            {
                error = newError;
                numerator = (int)fraction;
                denominator = (uint)i;
            }
        }
        return new Fraction(whole, numerator, denominator);
    }

    public static bool operator ==(Fraction a, Fraction b)
    {
        return a.whole == b.whole && a.numerator == b.numerator && a.denominator == b.denominator;
    }

    public static bool operator !=(Fraction a, Fraction b)
    {
        return !(a == b);
    }

    public static bool operator <(Fraction a, Fraction b)
    {
        return (double)a < (double)b;
    }

    public static bool operator >(Fraction a, Fraction b)
    {
        return (double)a > (double)b;
    }

    public static bool operator <=(Fraction a, Fraction b)
    {
        return (double)a <= (double)b;
    }

    public static bool operator >=(Fraction a, Fraction b)
    {
        return (double)a >= (double)b;
    }
}