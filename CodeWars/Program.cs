﻿// Copyright 2024 XLN Telecom, Inc. All rights reserved.
// This computer source code and related instructions and comments are the unpublished 
// confidential and proprietary information of XLN Telecom Ltd. and are protected under UK 
// and foreign intellectual property laws. They may not be disclosed to, copied or used by 
// any third party without the prior written consent of XLN Telecom Ltd.
// ----------------------------------------------------------------------------------------------------

namespace CodeWars;

public class CodeWars
{
    public static void Main(string[] args)
    {
        var arr1 = new[] { 13, 64, 15, 17, 88 };
        var arr2 = new[] { 23, 14, 53, 17, 80 };
        var test = _7Kyu.NumberPairs.NumberPairs.GetLargerNumbers(arr1, arr2);
        for (int i = 0; i < test.Length; i++)
        {
            Console.WriteLine(test[i]);
        }
    }
}