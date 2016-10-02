using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
/// Summary description for clsLogger
/// </summary>
public class clsLogger
{
	public clsLogger()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public static void messageLog(string msg)
    {
        string fileName = "log.txt";
        string path = HttpContext.Current.Server.MapPath("~/LOG/");

		if(!File.Exists(path+fileName))
		{
			File.Create(path+fileName).Dispose();
		}


        using (FileStream fs = new FileStream(path+fileName, FileMode.Append, FileAccess.Write))
        using (StreamWriter sw = new StreamWriter(fs))
        {
            sw.WriteLine(msg+"<br>");
        }
    }

    public static void messageLog(Object msg)
    {
        string fileName = "log.txt";
        string path = HttpContext.Current.Server.MapPath("~/LOG/");

        if (!File.Exists(path + fileName))
        {
            File.Create(path + fileName).Dispose();
        }


        using (FileStream fs = new FileStream(path + fileName, FileMode.Append, FileAccess.Write))
        using (StreamWriter sw = new StreamWriter(fs))
        {
            sw.WriteLine(msg.ToString() + "<br>");
        }
    }

    public static void errorLog(string msg)
    {
        string fileName = "error.txt";
        string path = HttpContext.Current.Server.MapPath("~/LOG/");

        if (!File.Exists(path + fileName))
        {
            File.Create(path + fileName).Dispose();
        }


        using (FileStream fs = new FileStream(path + fileName, FileMode.Append, FileAccess.Write))
        using (StreamWriter sw = new StreamWriter(fs))
        {
            sw.WriteLine(msg + "<hr>");
            CFunction cf = new CFunction();
            cf.MailTo("tarun.anand@eclerx.com", "keshav.singh@eclerx.com", "", "Unhandle Error : ","<pre>"+ msg+"</pre>");
        }
    }
}