using AddressSearchWebAPI.Models;
using AddressSearchWebAPI.Models.Requests;
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
        /// <summary>
        /// service Address
        /// </summary>
        /// <param name="addressSearchRequest"></param>
        /// <returns></returns>
        public async Task<List<AddressSearchModel>> SearchServiceAddress(AddressSearchRequestModel addressSearchRequest)
        {
            var addressSearchList = new List<AddressSearchModel>();
            await OpenDBConnectionIVRAsync("FrontierIVR");
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spSS_GetServiceAddressLike";
                command.Parameters.Add("@Name", SqlDbType.VarChar).Value = addressSearchRequest.SearchString;
                command.Parameters.Add("@ZipCode", SqlDbType.VarChar).Value = addressSearchRequest.ZipCode;
                command.Connection = ConFrontierIVR;
                command.CommandTimeout = 0;
                SqlDataReader reader;
                reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var addressSearchModel = new AddressSearchModel();
                    addressSearchModel.ServiceAddress = reader.GetValue<string>("ServiceAddress");
                    addressSearchModel.Address = reader.GetValue<string>("Address");
                    addressSearchModel.City = reader.GetValue<string>("CityName");
                    addressSearchModel.State = reader.GetValue<string>("StateName");
                    addressSearchModel.Zip = reader.GetValue<string>("ZIPCODE");
                    addressSearchModel.ESIID = reader.GetValue<string>("ESIID");
                    addressSearchModel.SiteStatus = reader.GetValue<string>("SiteStatus");
                    addressSearchModel.ServiceClass = reader.GetValue<string>("ServiceClass");
                    addressSearchModel.AMSMeterYN = reader.GetValue<string>("AM_YN");
                    addressSearchModel.ServiceClassDescription = reader.GetValue<string>("site_class_desc");
                    addressSearchModel.TDSPCode = reader.GetValue<string>("tdsp_code");
                    addressSearchModel.SwitchHoldYN = reader.GetValue<string>("switch_hold");

                    addressSearchModel.StreetNameLong = reader.GetValue<string>("StreetNameLong");
                    addressSearchModel.StreetName = reader.GetValue<string>("StreetName");
                    addressSearchModel.StreetNumber = reader.GetValue<string>("StreetNum");
                    addressSearchModel.AptNumber = reader.GetValue<string>("AptNum");
                    addressSearchList.Add(addressSearchModel);
                }

                reader.Close();
                ConFrontierIVR.Close();
            }
            catch (Exception exp)
            {
                ConFrontierIVR.Close();
                var errorModel = new ErrorModel
                {
                    FunName = "AddressSearch-ServiceAddress",
                    IpAddress = "",
                    ExceptionText = ExceptionToString(exp),
                    Request = ObjectToJson(addressSearchRequest),
                    Response = null,
                    BrandCode = "",
                    Source = addressSearchRequest.Source
                };
                await InsertCommonErrorLogAsync(errorModel);
            }

            return addressSearchList;
        }                
    }
}