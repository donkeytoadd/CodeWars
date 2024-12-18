// Copyright 2024 XLN Telecom, Inc. All rights reserved.
// This computer source code and related instructions and comments are the unpublished 
// confidential and proprietary information of XLN Telecom Ltd. and are protected under UK 
// and foreign intellectual property laws. They may not be disclosed to, copied or used by 
// any third party without the prior written consent of XLN Telecom Ltd.
// ----------------------------------------------------------------------------------------------------

namespace CodeWars._3Kyu;
public class BinomialExpansion
{
    public static string Expand(string expr)
    {
        var coefficientString = expr.Skip(1)
            .TakeWhile(x => !Char.IsLetter(x)).ToString();

        var a = coefficientString != null && coefficientString.Length == 0 ? 1 : 
            coefficientString == "-" ? 1 : int.Parse(coefficientString); 
        var x = Convert.ToChar(expr.IndexOf('x'));
        var powerIndex = Convert.ToChar(expr.IndexOf('^'));
        var b = int.Parse(expr[(expr.IndexOf(x) + 1)..expr.IndexOf(')')]);
        var n = int.Parse(expr[(expr.IndexOf(powerIndex) + 1)..]);

        var result = new List<string>();

        for (int k = 0; k <= n; k++)
        {
            var coefficient = BinomialCoefficient(n, k) * Math.Pow(a, n - k) * Math.Pow(b, k);

            if (coefficient == 0) continue;

            var term = coefficient == 1 && (n - k > 0) ? "" : coefficient.ToString();
            if (n - k > 0) term += "x";
            if (n - k > 1) term += $"^{n - k}";

            result.Add(term);
        }

        return string.Join("", result.Select((term, i) => i > 0 && !term.StartsWith("-") ? "+" + term : term));
        
    }

    public static long Factorial(long n)
    {
        return n == 0 ? 1 : n * Factorial(n - 1);
    }

    public static long BinomialCoefficient(long n, long k)
    {
        return Factorial(n) / (Factorial(k) * Factorial(n - k));
    }
}