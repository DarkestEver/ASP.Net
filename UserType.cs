using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserType
/// </summary>
public static class UserType
{
    public static string DEVELOPER = "1";
    public static string AUDITOR = "0";


}

public static class RequestStatus
{
    public static string Pending = "NULL";
    public static string Decline = "0";
    public static string Accept = "1";
    public static string Complete = "2";

    public static string getStatus(string Da)
    {
        if (Da == "")
        {
            return "Pending";
        }
        else if (Da == "0")
        {
            return "Decline";
        }
        else if (Da == "1")
        {
            return "Accept";
        }
        else if (Da == "2")
        {
            return "Complete";
        }

        return "";
    }
}



