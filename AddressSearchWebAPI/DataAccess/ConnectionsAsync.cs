using AddressSearchWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AddressSearchWebAPI.DataAccess
{
    public partial class DataAsync
    {
        SqlConnection ConFrontierIVR,ConnFrontierEnroll, mDBConnectionFUN;
        private async Task OpenDBConnectionEnrollAsync(string connectionName)
        {
            try
            {
                if (ConnFrontierEnroll == null || ConnFrontierEnroll.State != ConnectionState.Open)
                {
                    string connectionString;
                    connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
                    ConnFrontierEnroll = new SqlConnection(connectionString);
                    await ConnFrontierEnroll.OpenAsync();
                }
            }
            catch (Exception exp)
            {
                //logger("OpenDBConnectionEnrollAsync:: Exception: " + ExceptionToString(exp));
            }
        }

        private async Task OpenDBConnectionIVRAsync(string connectionName)
        {
            try
            {
                if (ConFrontierIVR == null || ConFrontierIVR.State != ConnectionState.Open)
                {
                    string connectionString;
                    connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
                    ConFrontierIVR = new SqlConnection(connectionString);
                    await ConFrontierIVR.OpenAsync();
                }
            }
            catch (Exception exp)
            {
                //logger("OpenDBConnectionIVRAsync:: Exception: " + ExceptionToString(exp));
            }
        }

        private async Task OpenDBConnectionFUNAsync(string connectionName)
        {
            try
            {
                if (mDBConnectionFUN == null || mDBConnectionFUN.State != ConnectionState.Open)
                {
                    string connectionString;
                    connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
                    mDBConnectionFUN = new SqlConnection(connectionString);
                    await mDBConnectionFUN.OpenAsync();
                }
            }
            catch (Exception exp)
            {
                //logger("OpenDBConnectionFUN:: Exception: " + ExceptionToString(exp));
            }
        }

    }
}