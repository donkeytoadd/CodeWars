// Copyright 2024 XLN Telecom, Inc. All rights reserved.
// This computer source code and related instructions and comments are the unpublished 
// confidential and proprietary information of XLN Telecom Ltd. and are protected under UK 
// and foreign intellectual property laws. They may not be disclosed to, copied or used by 
// any third party without the prior written consent of XLN Telecom Ltd.
// ----------------------------------------------------------------------------------------------------

namespace CodeWars._6Kyu.CalculateHypotenuse;

public class CalculateHypotenuseTest
{
    public static double CalculateHypotenuse(double a, double b)
    {
        try
        {
            if (double.IsNaN(a) || double.IsNaN(b) || a <= 0 || b <= 0)
            {
                throw new ArgumentException("Both values have to be positive integers.");
            }

            return Math.Round(Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2)), 3);
        }
        
        catch(ArgumentException ex)
        {
            Console.WriteLine("{0}: {1}", ex.GetType().Name, ex.Message);
            throw;
        }

    }
}