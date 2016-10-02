using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SessionHandler
/// </summary>
public class SessionHandler : System.Web.UI.Page
{
    public const string SESSION_NAME = "UserSession";
    public UserSessionData SessionData
    {
        get
        {
            UserSessionData sessionData = (UserSessionData)Session[SESSION_NAME];
            if (sessionData != null)
                return sessionData;
            else
            {
                Session.Clear();
                return null;
            }
        }
        set
        {
            Session[SESSION_NAME] = value;
        }
    }

    public SessionHandler(){
    }

    public bool isSessionActive()
    {
        bool status = false;
        if (this.SessionData != null) {
            status = true;
        }

        return status;
    }
}