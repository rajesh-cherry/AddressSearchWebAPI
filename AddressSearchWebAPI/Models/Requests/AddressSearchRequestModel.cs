using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AddressSearchWebAPI.Models.Requests
{
    /// <summary>
    /// 
    /// </summary>
    public class AddressSearchRequestModel : BaseRequestModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string SearchString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string ZipCode { get; set; }
    }
}