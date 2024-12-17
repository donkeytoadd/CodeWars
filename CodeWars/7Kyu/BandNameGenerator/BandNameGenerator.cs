// Copyright 2024 XLN Telecom, Inc. All rights reserved.
// This computer source code and related instructions and comments are the unpublished 
// confidential and proprietary information of XLN Telecom Ltd. and are protected under UK 
// and foreign intellectual property laws. They may not be disclosed to, copied or used by 
// any third party without the prior written consent of XLN Telecom Ltd.
// ----------------------------------------------------------------------------------------------------

namespace CodeWars._7Kyu.BandNameGenerator;

public class Kata
{
    public static string BandNameGenerator(string str)
    {
        var firstChar = char.ToUpper(str[0]);
        var restOfWord = str.Substring(1);
        string result;
        
        if (str[0] == str[str.Length - 1])
        {
            result = firstChar + restOfWord + restOfWord;
        }
        else
        {
            result = $"The {firstChar + restOfWord}";
        }
        
        return result;
    }
}