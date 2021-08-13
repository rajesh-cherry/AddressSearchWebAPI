using AddressSearchWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace AddressSearchWebAPI.DataAccess
{
    public partial class DataAsync
    {

        public string ExceptionToString(Exception ex)
        {
            var excp = ex.Message + Environment.NewLine + ex.StackTrace;
            if (ex.InnerException != null)
                excp += ex.InnerException.Message + Environment.NewLine + ex.InnerException.StackTrace;
            return excp;
        }

        public async Task InsertCommonErrorLogAsync(ErrorModel errorModel)
        {
            await OpenDBConnectionFUNAsync("FrontierRetail");
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.sp_InsertCommonErrorLog";
                command.Parameters.Add("@FunName", SqlDbType.Text).Value = errorModel.FunName;
                command.Parameters.Add("@IPAddress", SqlDbType.Text).Value = errorModel.IpAddress;
                command.Parameters.Add("@Account_no", SqlDbType.Text).Value = errorModel.AccountNo;
                command.Parameters.Add("@ReqData", SqlDbType.Text).Value = errorModel.Request;
                command.Parameters.Add("@ResData", SqlDbType.Text).Value = errorModel.Response;
                command.Parameters.Add("@ExceptionText", SqlDbType.Text).Value = errorModel.ExceptionText;
                command.Parameters.Add("@Source", SqlDbType.Text).Value = errorModel.Source;
                command.Parameters.Add("@BrandCode", SqlDbType.Text).Value = errorModel.BrandCode;
                command.Connection = mDBConnectionFUN;
                await command.ExecuteNonQueryAsync();
                mDBConnectionFUN.Close();
            }
            catch (Exception exp)
            {
                mDBConnectionFUN.Close();
            }
        }

        private string ObjectToJson(object obj)
        {
            var json = new JavaScriptSerializer().Serialize(obj);
            return json;
        }
    }
}