/*-------------------------------------------------------------------------------------------------
 * Class Name       : DB
 * Module           :Admin
 * Description      :This Class gives Methods for Login of Admin User 
 * Methods          :1)GetConnection()
 *                   2)GetData(SqlCommand cmd) 
 *                   3)GetData(string sql)
 *                   4)ExecuteNonQuery(SqlCommand cmd)
 *                   5)ExecuteScaler(SqlCommand cmd)
 *                   6)GetDataSet(SqlCommand cmd)
 * Class Files      : DB.cs
 * Developed by     :Tarun
 * Date             :15/06/2012
 * Modified By      :
 * Modi. Description:
 * ------------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for DB
/// </summary>
public class DB
{
    #region Decleration
    private static IsolationLevel m_isoLevel = IsolationLevel.ReadUncommitted;
    #endregion


    #region "DB Access Functions"
    /// <summary>
    /// define IsolationLevel
    /// </summary>
    /// <value></value>
    /// <returns>IsolationLevel</returns>
    /// <remarks></remarks>
    public static IsolationLevel IsolationLevel
    {
        get { return m_isoLevel; }
    }
    /// <summary>
    /// Gets Connection out of Web.config
    /// </summary>
    /// <returns>Returns SqlConnection</returns>
    /// <remarks></remarks>
    /// 
    public static SqlConnection GetConnection(string connectionString = "OOSE_WR_ConnectionString")
    {
       
        connectionString = "OOSE_WR_ConnectionString";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionString].ToString());
        conn.Open();
        return conn;

    }
    /// <summary>
    /// Gets data out of the database
    /// </summary>
    /// <param name="cmd">The SQL Command</param>
    /// <returns>DataTable with the results</returns>
    public static DataTable GetData(SqlCommand cmd)
    {
        bool errorOccured=false;
        DataTable dt = null;
                    using (DataSet ds = new DataSet())
            {
        try
        {

                if (cmd.Connection != null)
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        dt= ds.Tables[0];
                    }

                }
                else
                {
                    using (SqlConnection conn = GetConnection())
                    {
                        using (SqlTransaction trans = conn.BeginTransaction(m_isoLevel))
                        {
                            try
                            {
                                cmd.Transaction = trans;
                                using (SqlDataAdapter da = new SqlDataAdapter())
                                {
                                    da.SelectCommand = cmd;
                                    da.SelectCommand.Connection = conn;
                                    da.Fill(ds);
                                    dt = ds.Tables[0];
                                    trans.Commit();
                                }
                            }
                            catch (SqlException se)
                            {
                               // trans.Rollback();
                                throw se;
                            }
                            catch (IndexOutOfRangeException ioe)
                            {
                                trans.Rollback();
                                throw ioe;
                            }
                            finally
                            {
                                conn.Close();

                            }
                        }
                    }
                }
            }

        catch (TimeoutException te)
        {
            return null;
        }
        finally
        {
        }
        return dt;
            }
    }
    /// <summary>
    /// Gets data out of database using a plain text string command
    /// </summary>
    /// <param name="sql">string command to be executed</param>
    /// <returns>DataTable with results</returns>
    public static DataTable GetData(string sql)
    {
        try
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlTransaction trans = conn.BeginTransaction(m_isoLevel))
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = sql;
                            using (DataSet ds = new DataSet())
                            {
                                using (SqlDataAdapter da = new SqlDataAdapter())
                                {
                                    da.SelectCommand = cmd;
                                    da.SelectCommand.Connection = conn;
                                    da.Fill(ds);
                                    return ds.Tables[0];
                                }
                            }
                        }
                    }
                    finally
                    {
                        trans.Commit();
                        conn.Close();
                    }
                }
            }
        }
        finally
        {

        }
    }
    /// <summary>
    /// Executes a NonQuery
    /// </summary>
    /// <param name="cmd">NonQuery to execute</param>
    public static void ExecuteNonQuery(SqlCommand cmd)
    {
        try
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlTransaction trans = conn.BeginTransaction(m_isoLevel))
                {
                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                conn.Close();
            }
        }
        finally
        {

        }
    }
    /// <summary>
    /// Executes a Scalar Query
    /// </summary>
    /// <param name="cmd">Scalar to execute</param>
    public static object ExecuteScalar(SqlCommand cmd)
    {
        try
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlTransaction trans = conn.BeginTransaction(m_isoLevel))
                {
                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    object res = cmd.ExecuteScalar();
                    trans.Commit();
                    conn.Close();
                    return res;
                }

            }
        }
        finally
        {

        }
    }
    /// <summary>
    /// Gets data out of the database
    /// </summary>
    /// <param name="cmd">The SQL Command</param>
    /// <returns>DataSet with the results</returns>
    public static DataSet GetDataSet(SqlCommand cmd)
    {
        try
        {
            if (cmd.Connection != null)
            {
                using (DataSet ds = new DataSet())
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
            else
            {
                using (SqlConnection conn = GetConnection())
                {
                    using (SqlTransaction trans = conn.BeginTransaction(m_isoLevel))
                    {
                        try
                        {
                            cmd.Transaction = trans;
                            using (DataSet ds = new DataSet())
                            {
                                using (SqlDataAdapter da = new SqlDataAdapter())
                                {
                                    da.SelectCommand = cmd;
                                    da.SelectCommand.Connection = conn;
                                    da.Fill(ds);
                                    return ds;
                                }
                            }
                        }
                        finally
                        {
                            trans.Commit();
                            conn.Close();
                        }
                    }
                }

            }
        }
        finally
        {

        }
    }
    #endregion

    public DB()
    {
        
    }
}

