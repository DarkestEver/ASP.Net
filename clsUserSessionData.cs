using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

/// <summary>
/// Summary description for Session
/// </summary>
/// 

[Serializable()]
public class UserSessionData
{
    public string EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeEmail { get; set; }
    public string EmployeeManager { get; set; }
    public string Designation { get; set; }
    public string DesignationCode { get; set; }
    public string UserRole { get; set; }
    public bool isAdmin { get; set; }
    public bool isManager { get; set; }
    public bool isAuditor { get; set; }
    public bool isDeveloper { get; set; }
    public bool isSME { get; set; }
    public bool isClient { get; set; }

    public UserSessionData()
	{
        EmployeeId = "";
        EmployeeName = "";
        EmployeeEmail = "";
        EmployeeManager = "";
        Designation = "";
        DesignationCode = "";
        UserRole = "";
        isAdmin = false;
        isManager = false;
        isAuditor = false;
        isDeveloper = false;
        isSME = false;
        isClient = false;
	}

    public void getUserRole()
    {
        if (this == null || this.DesignationCode == null)
        {
            return;
        }
        int desg = Convert.ToInt32(this.DesignationCode);
        if (desg <= 6)
        {
            this.UserRole = "hm"; // Higher Management
        }
        else if ((desg == 7) || (desg == 8))
        {
            this.UserRole = "mn"; // Manager
        }
        else if ((desg < 15) && (desg > 8))
        {
            this.UserRole = "de"; // Developer
        }
        else if (desg == 99)
        {
            this.UserRole = "cl"; // Client
        }
        else
        {
            this.UserRole = "de"; // Developer
        }
    }
}