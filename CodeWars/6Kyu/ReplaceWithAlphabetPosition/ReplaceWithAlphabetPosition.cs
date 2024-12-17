// Copyright 2024 XLN Telecom, Inc. All rights reserved.
// This computer source code and related instructions and comments are the unpublished 
// confidential and proprietary information of XLN Telecom Ltd. and are protected under UK 
// and foreign intellectual property laws. They may not be disclosed to, copied or used by 
// any third party without the prior written consent of XLN Telecom Ltd.
// ----------------------------------------------------------------------------------------------------

namespace CodeWars._6Kyu.ReplaceWithAlphabetPosition;

public class ReplaceWithAlphabetPosition
{
    public static string AlphabetPosition(string text)
    {
        var result = "";
        
        var alphabetMatches = new Dictionary<char, int>();
        
        for(char c = 'A'; c <= 'Z'; c++) {
            int key = c - 'A' + 1;
            alphabetMatches.Add(c,key);
        }
        
        foreach (var c in text)
        {
            char upperChar = char.ToUpper(c);

            if (alphabetMatches.ContainsKey(upperChar))
            {
                result += alphabetMatches[upperChar] + " ";
            }
        }
        
        return result.TrimEnd();
    }
}