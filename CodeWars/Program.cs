// Copyright 2024 XLN Telecom, Inc. All rights reserved.
// This computer source code and related instructions and comments are the unpublished 
// confidential and proprietary information of XLN Telecom Ltd. and are protected under UK 
// and foreign intellectual property laws. They may not be disclosed to, copied or used by 
// any third party without the prior written consent of XLN Telecom Ltd.
// ----------------------------------------------------------------------------------------------------

namespace CodeWars;

using _3Kyu;

public class CodeWars
{
    public static void Main(string[] args)
    {
        string str = "(2x+3)^3";
        var result = BinomialExpansion.Expand(str);
        Console.WriteLine(result);

    }
}