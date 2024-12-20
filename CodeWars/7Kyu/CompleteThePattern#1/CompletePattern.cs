﻿// Copyright 2024 XLN Telecom, Inc. All rights reserved.
// This computer source code and related instructions and comments are the unpublished 
// confidential and proprietary information of XLN Telecom Ltd. and are protected under UK 
// and foreign intellectual property laws. They may not be disclosed to, copied or used by 
// any third party without the prior written consent of XLN Telecom Ltd.
// ----------------------------------------------------------------------------------------------------

namespace CodeWars._7Kyu.CompleteThePattern_1;

public class CompletePattern
{
    public string Pattern(int n)
    {
        if (n < 1)
        {
            return string.Empty;
        }

        string result = string.Empty;

        for (int i = 1; i <= n; i++)
        {
            string repeated = new string(' ', i).Replace(" ", i.ToString());
            result += repeated;

            if (i < n)
            {
                result += "\n";
            }
        }

        return result;
    }
}