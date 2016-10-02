using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Runtime.Serialization;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for ruid_unid
/// </summary>


// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[WebService(Description = "Web services to update developers,auditors,testlinks,livelinks", Namespace = "http://oose.eclerx.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class ruid_unid : System.Web.Services.WebService {

    public ruid_unid () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true)]
    public string unidruid(string ruid, string unid, string value)
    {
        UserSessionData usd;

        try
        {
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //string strJSON = js.Serialize(clsRijndaelSimple.Decrypt(unid));
            //return strJSON;
            string empid = "0";
            try
            {
                usd = (UserSessionData)HttpContext.Current.Session[SessionHandler.SESSION_NAME];
                empid = usd.EmployeeId;
            }
            catch (NullReferenceException)
            {
                empid = "0";
                return "Session Expired";
            }

            try
            {
                
                if (Convert.ToInt32(usd.DesignationCode) > 8 || usd.isClient)
                {
                    return "You are not authorised.";
                }
            }
            catch (FormatException) { return "Invalid Desingation code. pls contact  tool admin."; }



            using (System.Data.DataTable dt = clsAppPara.ruid_update(clsRijndaelSimple.Decrypt(unid), value, ruid, empid, "A"))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["msg"].ToString() == "2")
                    {
                        return value;
                    }
                    else
                    {
                        return "invalid User Name";
                    }
                }
                else
                {
                    return "invalid Request";
                }
            }
        }
        catch (Exception)
        {
            return "Something went wrong. error reported to the admin.";
        }

        return value;
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true)]
    public string ajaxUpdate(string ruid, string unid, string value)
    {
         UserSessionData usd;
        //JavaScriptSerializer js = new JavaScriptSerializer();
        //string strJSON = js.Serialize(clsRijndaelSimple.Decrypt(unid));
        //return strJSON;
        string empid = "0";
        try
        {
            usd= (UserSessionData)HttpContext.Current.Session[SessionHandler.SESSION_NAME];
            empid = usd.EmployeeId;
        }
        catch (NullReferenceException)
        {
            empid = "0";
            return "Session Expired";
        }
        try
        {
            if (Convert.ToInt32(usd.DesignationCode) > 8)
            {
                return "You are not authorised.";
            }
        }
        catch (FormatException) { return "Invalid Desingation code. pls contact  tool admin."; }
        string flag = "B";
        if (clsRijndaelSimple.Decrypt(unid).ToLower() == "vcmisc")
        {
            flag = "C";
        }
        else if (clsRijndaelSimple.Decrypt(unid).ToLower() == "vclink")
        {
            flag = "D";
        }
        else
        {
            flag = "B";
        }
        using (System.Data.DataTable dt = clsAppPara.ruid_update(clsRijndaelSimple.Decrypt(unid), value, ruid, empid, flag))
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["msg"].ToString() == "1")
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string jsonstring=js.Serialize(value);
                    return value;
                }
                else
                {
                    return "Invalid Input Please try Again";
                }
            }
            else
            {
                
                return "invalid Request";
            }
        }

        return value;
    }
    
}
