using AddressSearchWebAPI.DataAccess;
using AddressSearchWebAPI.Models;
using AddressSearchWebAPI.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AddressSearchWebAPI.Controllers
{
    [RoutePrefix("api")]
    public class AddressSearchController : ApiController
    {
        private readonly DataAsync DataLayerAsync;

        /// <summary>
        /// 
        /// </summary>
        public AddressSearchController()
        {
            DataLayerAsync = new DataAsync();
        }

        /// <summary>
        /// Search the service address.
        /// </summary>
        /// <param name="addressSearchRequest"></param>
        /// <returns></returns>
        /// <remarks>Searches the service address.</remarks>
        /// <response code="200">A list of service addresses are returned.</response>
        /// <response code="400">The request to search address is invalid. Null or empty request parameters.</response>
        /// <response code="500">Oops! Could not search service address. Check the error logs.</response>
        [HttpPost]
        [Route("AddressSearch")]
        [ResponseType(typeof(List<AddressSearchModel>))]
        public async Task<IHttpActionResult> AddressSearch([FromBody]AddressSearchRequestModel addressSearchRequest)
        {
            #region Request Validation

            var validationErrors = new List<string> { };

            if (string.IsNullOrWhiteSpace(addressSearchRequest.SearchString)
                || string.IsNullOrWhiteSpace(addressSearchRequest.ZipCode))
            {
                validationErrors.Add("Invalid Request.");
            }

            //if (string.IsNullOrWhiteSpace(addressSearchRequest.BrandCode))
            //{
            //    validationErrors.Add("BrandCode cannot be Empty.");
            //}

            ////if (string.IsNullOrWhiteSpace(addressSearchRequest.Source))
            ////{
            ////    validationErrors.Add("Source cannot be Empty.");
            ////}

            if (validationErrors.Any())
            {
                return BadRequest(string.Join(Environment.NewLine, validationErrors));
            }

            #endregion Request Validation

            var addressSearchResult = await DataLayerAsync.SearchServiceAddress(addressSearchRequest);
            return Ok(addressSearchResult);
        }

        
    }
}
