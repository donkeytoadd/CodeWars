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
        var coefficient = expr.Skip(1)
            .TakeWhile(x => !Char.IsLetter(x));

        var a = coefficient.ToString().Length == 0 ? 1 : 
            coefficient.ToString() == "-" ? 1 : int.Parse(coefficient.ToString()); 
        var x = Convert.ToChar(expr.IndexOf('x'));
        var powerIndex = Convert.ToChar(expr.IndexOf('^'));
        var b = int.Parse(expr[(expr.IndexOf(x) + 1)..expr.IndexOf(')')]);
        var n = int.Parse(expr[(expr.IndexOf(powerIndex) + 1)..]);
        
        
        
        return "";
    }
}