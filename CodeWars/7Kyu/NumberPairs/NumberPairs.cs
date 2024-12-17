// Copyright 2024 XLN Telecom, Inc. All rights reserved.
// This computer source code and related instructions and comments are the unpublished 
// confidential and proprietary information of XLN Telecom Ltd. and are protected under UK 
// and foreign intellectual property laws. They may not be disclosed to, copied or used by 
// any third party without the prior written consent of XLN Telecom Ltd.
// ----------------------------------------------------------------------------------------------------

namespace CodeWars._7Kyu.NumberPairs;

public class NumberPairs
{
    public static int [] GetLargerNumbers(int [] a , int [] b)
    {
        var tempList = new List<int>();
        for (int i = 0; i <= a.Length - 1 && i <= b.Length - 1; i++)
        {
            tempList.Add(a[i] > b[i] ? a[i] : b[i]);
        }
        return tempList.ToArray();
    }
}