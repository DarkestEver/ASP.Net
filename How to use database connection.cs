using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for clsETT
/// </summary>
public class clsETT
{
	public clsETT()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable GetData(
        object intEETId
      , object intRequestID
      , object intTimeCatID
      , object intTimeChagred
      , object intVolumeCharged
      , object intCreatedBy
      , object vcComments
      , object bitIsActive
      , object transFlag
      , object dtCreatedDate= null
      , object ftWorkUnit = null
    )
    {
        SqlCommand cmd = new SqlCommand("usp_ett");
        {
            cmd.CommandType = CommandType.StoredProcedure;
            {
                cmd.Parameters.AddWithValue("@intEETId", intEETId);
                cmd.Parameters.AddWithValue("@vcComments", vcComments);
                cmd.Parameters.AddWithValue("@intRequestID", intRequestID);
                cmd.Parameters.AddWithValue("@intTimeCatID", intTimeCatID);
                cmd.Parameters.AddWithValue("@intTimeChagred", intTimeChagred);
                cmd.Parameters.AddWithValue("@intVolumeCharged", intVolumeCharged);
                cmd.Parameters.AddWithValue("@intCreatedBy", intCreatedBy);
                cmd.Parameters.AddWithValue("@bitIsActive", bitIsActive);
                cmd.Parameters.AddWithValue("@transFlag", transFlag);
                cmd.Parameters.AddWithValue("@dtCreatedDate",dtCreatedDate);
                cmd.Parameters.AddWithValue("@ftWorkUnit", ftWorkUnit);
                return DB.GetData(cmd);
            }
        }
    }

    

    public static object  TotalTimeCharged( object intCreatedBy   , object transFlag  )
    {
        SqlCommand cmd = new SqlCommand("usp_ett");
        {
            cmd.CommandType = CommandType.StoredProcedure;
            {
                cmd.Parameters.AddWithValue("@intCreatedBy", intCreatedBy);
                cmd.Parameters.AddWithValue("@transFlag", transFlag);
                return  DB.ExecuteScalar(cmd);
            } 
        }
    }

   
}
